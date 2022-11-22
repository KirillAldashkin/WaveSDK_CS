namespace VRGeomCS;

public class FrameBufferObject
{
    private bool mMSAA = false;
    private int mWidth = 0;
    private int mHeight = 0;
    private float mScale = 1.0f;
    private int mScaledWidth = 0;
    private int mScaledHeight = 0;
    private int mViewportX = 0;
    private int mViewportY = 0;
    private uint mViewportWidth = 0;
    private uint mViewportHeight = 0;
    private uint mRenderFrameBufferId = 0;
    private uint mFrameBufferId = 0;
    private uint mDepthBufferId = 0;
    private uint mRenderTextureId = 0;
    private uint mTextureId = 0;
    private bool mHasError = false;

    public FrameBufferObject(int textureId, int width, int height, bool msaa = false);

    public static FrameBufferObject? GetFBOInstance(int width, int height)
    {
        var fbo = new FrameBufferObject(0, width, height);
        return fbo.mFrameBufferId == 0 ? null : fbo;
    }

    public ~FrameBufferObject();
    public void initMSAA();
    public void init();

    public void bindTexture()
    {
        glBindTexture(GL_TEXTURE_2D, mTextureId);
    }

    public void unbindTexture()
    {
        glBindTexture(GL_TEXTURE_2D, 0);
    }

    public void bindFrameBuffer();
    public void unbindFrameBuffer();

    public void glViewportFull()
    {
        glViewport(0, 0, mWidth, mHeight);
    }

    public uint TextureID => mTextureId;

    public bool HasError => mHasError;

    public void clear();

    public void glViewportScale(std::vector<float> lowerLeft, std::vector<float> topRight);

    public void resizeFrameBuffer(float scale);
}