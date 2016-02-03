using UnityEngine;
//序列帧动画
public class MovieClip : MonoBehaviour
{
    //sprite渲染
    private SpriteRenderer sr = null;
    //动画数组    
    private Object[] aniAry = null;
    //运行间隔
    private float delay;
    //当前运行时间
    private float curTime;
    //是否循环播放
    private int loop = 1;
    //当前循环次数
    private int curLoop = 1;
    //当前帧
    private int _currentframe = 1;
    public int currentframe { get { return _currentframe; } }
    //总帧数
    private int _totalFrames = 1;
    public int totalFrames { get { return _totalFrames; } }
    //是否在运行
    private bool _isPlaying = false;
    public bool isPlaying { get { return _isPlaying; }}
    //运行帧频
    public int fps = 30;
    //是否自动销毁
    public bool autoDestroy = false;
    /// <summary>
    /// 初始化
    /// </summary>
    void Awake()
    {
        this.sr = this.gameObject.GetComponent<SpriteRenderer>();
        if (this.sr == null)
            this.sr = this.gameObject.AddComponent<SpriteRenderer>();
        this.delay = 1 / (float)(this.fps);
        this._currentframe = 1;
        this.curLoop = 0;
    }

    /// <summary>
    /// 加载Resources下某个目录中的动画资源
    /// </summary>
    /// <param name="path">路径名</param>
    public void loadPathRes(string path)
    {
        if (aniAry == null)
            aniAry = Resources.LoadAll(path);
        this._totalFrames = aniAry.Length;
    }

    /// <summary>
    /// 播放
    /// </summary>
    /// <param name="loop">循环次数 -1为无限循环</param>
    public void play(int loop = 1)
    {
        this.loop = loop;
        this._isPlaying = true;
    }

    /// <summary>
    /// 暂停
    /// </summary>
    public void stop()
    {
        this._isPlaying = false;
    }

    /// <summary>
    /// 跳转停止到某一帧
    /// </summary>
    /// <param name="frame">帧</param>
    public void gotoAndStop(int frame)
    {
        this.stop();
        if (frame < 1) frame = 1;
        if (frame > this._totalFrames) frame = this._totalFrames;
        this._currentframe = frame;
        this.render();
    }

    /// <summary>
    /// 从某一帧开始播放
    /// </summary>
    /// <param name="frame">开始播放的帧</param>
    /// <param name="loop">循环次数</param>
    public void gotoAndPlay(int frame, int loop = 1)
    {
        this.play(loop);
        if (frame < 1) frame = 1;
        if (frame > this._totalFrames) frame = this._totalFrames;
        this._currentframe = frame;
        this.render();
    }

    /// <summary>
    /// 渲染当前帧
    /// </summary>
    public void render()
    {
        if (this.sr == null) return;
        if (this.aniAry == null) return;
        if (this.aniAry.Length == 0) return;
        GameObject spt = aniAry[this._currentframe - 1] as GameObject;
        this.sr.sprite = spt.GetComponent<SpriteRenderer>().sprite;
    }

    /// <summary>
    /// 帧循环
    /// </summary>
    void FixedUpdate()
    {
        if (this.aniAry == null) return;
        if (this.aniAry.Length == 0) return;
        if (!this._isPlaying) return;
        this.curTime += Time.deltaTime;
        if (this.curTime >= this.delay)
        {
            this.curTime = 0;
            this._currentframe++;
            if(this.loop < 0)
            {
                if (this._currentframe > this._totalFrames)
                    this._currentframe = 1;
            }
            else
            {
                if (this._currentframe > this._totalFrames)
                {
                    this._currentframe = 1;
                    this.curLoop++;
                    if (this.curLoop >= this.loop)
                    {
                        if (this.autoDestroy)
                        {
                            this.aniAry = null;
                            GameObject.Destroy(this.gameObject);
                            return;
                        }
                        else
                        {
                            this.stop();
                        }
                    }
                }
            }
            this.render();
        }
    }

    /// <summary>
    /// 设置缩放
    /// </summary>
    /// <param name="scale">缩放比例</param>
    public void setScale(float scale)
    {
        this.transform.localScale = new Vector3(scale, scale, scale);
    }
}
