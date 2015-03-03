#if !NETFX_CORE
namespace System.IO.Compression {
    internal enum BlockType {
        Uncompressed = 0,
        Static = 1,
        Dynamic = 2
    }
}
#endif
