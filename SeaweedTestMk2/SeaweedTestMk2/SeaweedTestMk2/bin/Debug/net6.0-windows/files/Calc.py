import PyQt5.QtWidgets as qtw

class MainWindow(qtw.QDockWidget):
    def __init__(self):
        super().__init__()
        self.setWindowTitle('Calculator')
        self.setLayout(qtw.QVBoxLayout())
        self.keypad()

        self.show()
    def keypad(self):
        container = qtw.QWidget()
        container.setLayout(qtw.QGridLayout())

        #Buttons
        result_field = qtw.QLineEdit

        btn_result = qtw.QPushButton('Enter')
        btn_clear = qtw.QPushButton('Clear')

        btn_9 = qtw.QPushButton('9')
        btn_8 = qtw.QPushButton('8')
        btn_7 = qtw.QPushButton('7')
        btn_6 = qtw.QPushButton('6')
        btn_5 = qtw.QPushButton('5')
        btn_4 = qtw.QPushButton('4')
        btn_3 = qtw.QPushButton('3')
        btn_2 = qtw.QPushButton('2')
        btn_1 = qtw.QPushButton('1')
        btn_0 = qtw.QPushButton('0')

        btn_plus = qtw.QPushButton('+')
        btn_mins = qtw.QPushButton('-')
        btn_mult = qtw.QPushButton('*')
        btn_div = qtw.QPushButton('/')




app = qtw.QApplication([])
mw = MainWindow()
app.exec_()
