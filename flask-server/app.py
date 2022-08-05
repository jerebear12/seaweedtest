import sqlite3
import jwt
import uuid
import re
import datetime
from functools import wraps
from flask import Flask, request, jsonify, make_response
from flask_sqlalchemy import SQLAlchemy
from flask_bcrypt import Bcrypt
from flask_login import current_user, UserMixin, LoginManager
from flask_jwt import JWT, jwt_required, current_identity
from werkzeug.security import safe_str_cmp, generate_password_hash, check_password_hash

### Initialization of app and globals ###
app = Flask(__name__)
app.config['SECRET_KEY'] = 'owls-only-come-out-at-night'
app.config['REFRESH_TOKEN'] = 'night-time-is-dark'
app.config['SQLALCHEMY_DATABASE_URI'] = 'sqlite:///users.db'
db = SQLAlchemy()
db.init_app(app)
bcrypt = Bcrypt()
login_manager = LoginManager()
login_manager.init_app(app)

### db table in Model form ###
class User(db.Model, UserMixin):
    id = db.Column(db.Integer, primary_key=True)
    username = db.Column(db.String(20), unique=True, nullable=False)
    email = db.Column(db.String(120), unique=True, nullable=False)
    password = db.Column(db.String(60), nullable=False)

### Login manager allows current_user attribute to be used ###
@login_manager.user_loader
def load_user(user_id):
    return User.query.get(int(user_id))

### Utils for JWT ###
def authenticate(username, password):
    user = User.query.filter_by(username=username).first()
    if user and safe_str_cmp(user.password.encode('utf-8'), password.encode('utf-8')):
        return user

def identity(payload):
    return User.query.filter(User.id == payload['identity']).scalar()

### Init JWT ###
json_web_token = JWT(app, authenticate, identity)

### Token required wrap function ###
def token_required(f):
   @wraps(f)
   def decorator(*args, **kwargs):
       token = None
       if 'x-access-tokens' in request.headers:
           token = request.headers['x-access-tokens']
 
       if not token:
           return jsonify({'message': 'No Token'})
       try:
           data = jwt.decode(token, app.config['SECRET_KEY'], algorithms=["HS256"])
           current_user = User.query.filter_by(id=data['id']).first()
       except:
           return jsonify({'message': 'Token is Invalid'})
 
       return f(current_user, *args, **kwargs)
   return decorator

### Routes ###
@app.route("/",  methods=['GET'])
def hello_world():
    return "<p>Hello, World!</p>"

### Works ###
@app.route('/signup', methods=['POST'])
def signup_user(): 
    data = request.get_json()
    if data:
        if User.query.filter_by(username=data['username']).first():
            return jsonify({'error': 'Duplicate username'})
        elif User.query.filter_by(email=data['email']).first():
            return jsonify({'error': 'Duplicate email'})
        
        if not re.match(r"[^@]+@[^@]+\.[^@]+", data['email']):
            return jsonify({'error': 'Invalid email'})

        hashed_password = generate_password_hash(data['password'], method='sha256')
        user = User(username=data['username'], email=data['email'], password=hashed_password)
        db.session.add(user) 
        db.session.commit()  
    else:
        return jsonify({
            'error': 'No Request Data',
            'data': data
        })
    
    return jsonify({'success': 'Registered Successfully'})
    

### Works ###
@app.route("/login", methods=["POST"])
def login():
    if current_user.is_authenticated:
        return {
            'error': 'You are already logged in!'
        }
    data = request.get_json()
    if data:
        print(data)
        # Log in
        # Get first user by email return bool
        if 'email' in data:
            user = User.query.filter_by(email=data['email']).first()
        else:
            user = User.query.filter_by(username=data['username']).first()
        # If user exists and password entered in the form matches the password in the database
        if user and check_password_hash(user.password, data['password']):
            # Pass user and form remember me checkbox bool
            token = jwt.encode({'id' : user.id, 'exp' : datetime.datetime.utcnow() + datetime.timedelta(minutes=5)}, app.config['SECRET_KEY'], "HS256")
            refresh_token = jwt.encode({'id' : user.id, 'exp' : datetime.datetime.utcnow() + datetime.timedelta(days=1)}, app.config['REFRESH_TOKEN'], "HS256")
            ### For testing only
            print("Success")
            print(refresh_token)
            return jsonify({
                'token' : token.decode("utf-8"),
                'refresh_token': refresh_token.decode("utf-8"),
                'success': 'Login Successful'
            })
        else:
            return {
                'error': 'Login Unsuccessful'
            }
    else:
        return {
            'error': 'No Data Found'
        }

### Works ###
@app.route('/users', methods=['GET'])
def get_all_users(): 
 
   users = User.query.all()
   result = []  
   for user in users:  
       user_data = {}  
       user_data['id'] = user.id 
       user_data['username'] = user.username
       user_data['password'] = user.password
     
       result.append(user_data)  
   return jsonify({'users': result})

### Works ###
@app.route("/refresh", methods=["POST"])
def refresh():
    data = request.get_json()
    if data:
        print(data)
        # Log in
        # Get first user by email return bool
        if 'refresh_token' in data:
            re_token = data['refresh_token']
            print(re_token)
            try:
                decoded_re_token = jwt.decode(re_token, app.config['REFRESH_TOKEN'], algoritthms=['HS256'])
                if 'id' in decoded_re_token:
                    # See if user in JWT exists
                    user = User.query.filter_by(id=decoded_re_token['id']).first()
                    if user != []:
                        token = jwt.encode({'id' : user.id, 'exp' : datetime.datetime.utcnow() + datetime.timedelta(minutes=5)}, app.config['SECRET_KEY'], "HS256")
                        return jsonify({
                            'token' : token.decode("utf-8"),
                            'success': 'Refresh Successful'
                        })
                    else:
                        return jsonify({
                            'error': 'User does not exist'
                        })
                else:
                    # No id in decoded_re_token
                    print("No id in decoded token")
                    print(jsonify(decoded_re_token))
            except jwt.exceptions.InvalidTokenError as base_invalid:
                print("Failed to decode, err:")
                print(base_invalid)
                return {
                    'error': 'Invalid Token'
                }
        else:
            # No token in POST
            return jsonify({
                'error': 'No JWT Provided'
            })
    else:
        return {
            'error': 'No Data Found'
        }

### Run app ###
if __name__ == "__main__":
    app.run(debug=True)
