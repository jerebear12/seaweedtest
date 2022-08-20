using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeaweedTestMk2
{
    // PyAPIResp myDeserializedClass = JsonConvert.DeserializeObject<PyAPIResp>(myJsonResponse);
    public class PyAPIResp
    {
        public string? token { get; set; }
        public string? refresh_token { get; set; }
        public string? error { get; set; }
        public string? success { get; set; }
    }
    // FilerGetResp myDeserializedClass = JsonConvert.DeserializeObject<FilerGetResp>(myJsonResponse);
    public class Chunk
    {
        public string? file_id { get; set; }
        public long? size { get; set; }
        public long? mtime { get; set; }
        public string? e_tag { get; set; }
        public Fid? fid { get; set; }
        public bool? is_gzipped { get; set; }
    }

    public class Entry
    {
        public string? FullPath { get; set; }
        public DateTime? Mtime { get; set; }
        public DateTime? Crtime { get; set; }
        public long? Mode { get; set; }
        public int? Uid { get; set; }
        public int? Gid { get; set; }
        public string? Mime { get; set; }
        public string? Replication { get; set; }
        public string? Collection { get; set; }
        public int? TtlSec { get; set; }
        public string? UserName { get; set; }
        public object? GroupNames { get; set; }
        public string? SymlinkTarget { get; set; }
        public object? Md5 { get; set; }
        public object? Extended { get; set; }
        public List<Chunk>? chunks { get; set; }
    }

    public class Fid
    {
        public int volume_id { get; set; }
        public int file_key { get; set; }
        public Int64 cookie { get; set; }
    }

    public class FilerGetResp
    {
        public string? Path { get; set; }
        public List<Entry>? Entries { get; set; }
        public int? Limit { get; set; }
        public string? LastFileName { get; set; }
        public bool? ShouldDisplayLoadMore { get; set; }
    }

    public class FilerPostResp
    {
        public string? name { get; set; }
        public long? size { get; set; }
        public string? fid { get; set; }
        public string? url { get; set; }
    }

    public class ChunkInfo
    {
        public string? fid { get; set; }
        public int? offset { get; set; }
        public long? size { get; set; }
    }

    public class ChunkManifest
    {
        public string? name { get; set; }
        public string? mime { get; set; }
        public long? size { get; set; }
        public List<ChunkInfo>? chunks { get; set; }
    }

    public class FilerGetMetadata
    {
        public string? FullPath { get; set; }
        public DateTime? Mtime { get; set; }
        public DateTime? Crtime { get; set; }
        public int? Mode { get; set; }
        public int? Uid { get; set; }
        public int? Gid { get; set; }
        public string? Mime { get; set; }
        public string? Replication { get; set; }
        public string? Collection { get; set; }
        public int? TtlSec { get; set; }
        public string? DiskType { get; set; }
        public string? UserName { get; set; }
        public object? GroupNames { get; set; }
        public string? SymlinkTarget { get; set; }
        public string? Md5 { get; set; }
        public long? FileSize { get; set; }
        public object? Extended { get; set; }
        public List<Chunk>? chunks { get; set; }
        public object? HardLinkId { get; set; }
        public int? HardLinkCounter { get; set; }
        public object? Content { get; set; }
        public object? Remote { get; set; }
        public int? Quota { get; set; }
    }
}
