#if !NETFX_CORE
namespace Unity.IO.Compression {
    internal enum BlockType {
        Uncompressed = 0,
        Static = 1,
        Dynamic = 2
    }
}
#endif
