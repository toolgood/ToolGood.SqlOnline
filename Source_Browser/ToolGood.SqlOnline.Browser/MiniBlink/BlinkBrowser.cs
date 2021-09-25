using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using MiniBlinkPinvoke;
using ToolGood.SqlOnline.Browser.Core;

namespace ToolGood.MyBrowser
{
    public class BlinkBrowser : Control, IMessageFilter
    {

        public static string CookiePath { get; set; }
        public static string LocalStoragePath { get; set; }

        public IntPtr handle = IntPtr.Zero;
        string url = string.Empty;
        public object GlobalObjectJs = null;

        public delegate void TitleChange(string title);
        public event TitleChange OnTitleChangeCall;

        UrlChangedCallback urlChangedCallback;
        public delegate void URLChange(string url);
        public event URLChange OnUrlChangeCall;

        //public delegate void URLChange2(string url);
        //public event URLChange2 OnUrlChange2Call;

        public delegate void OnUrlNavigation(string url);
        public event OnUrlNavigation OnUrlNavigationCall;

        public delegate void DocumentReady();
        public event DocumentReady DocumentReadyCallback;

        public delegate IntPtr OnCreateViewCallback(IntPtr webView, IntPtr param, wkeNavigationType navigationType, string url);
        public event OnCreateViewCallback OnCreateViewEvent;

        //URL end 回调通知
        public delegate void OnUrlEnd(byte[] bytes, string url, int len);
        public event OnUrlEnd OnUrlEndEvent;

        //下载事件回调通知
        public delegate void OnDownload(string url);
        public event OnDownload OnDownloadFile;


        wkeOnShowDevtoolsCallback _wkeOnShowDevtoolsCallback;

        public void ShowDevtools(string path)
        {
            BlinkBrowserPInvoke.wkeShowDevtools(this.handle, path, _wkeOnShowDevtoolsCallback, IntPtr.Zero);
        }

        /// <summary>
        /// 页面是否加载失败
        /// </summary>
        public bool IsLoadingFailed {
            get {
                if (handle != IntPtr.Zero) {
                    return BlinkBrowserPInvoke.wkeIsLoadingFailed(handle);
                }
                return false;
            }
        }
        /// <summary>
        /// 页面是否加载成功
        /// </summary>
        public bool IsLoadingSucceeded {
            get {
                if (handle != IntPtr.Zero) {
                    return BlinkBrowserPInvoke.wkeIsLoadingSucceeded(handle);
                }
                return false;
            }
        }
        public bool IsLoadingCompleted {
            get {
                if (handle != IntPtr.Zero) {
                    return BlinkBrowserPInvoke.wkeIsLoadingCompleted(handle);
                }
                return false;
            }
        }

        List<object> listObj = new List<object>();

        AlertBoxCallback AlertBoxCallback;
        TitleChangedCallback titleChangeCallback;
        TitleChangedCallback titleChangeCallback2;
        wkeNavigationCallback _wkeNavigationCallback;
        wkeConsoleMessageCallback _wkeConsoleMessageCallback;
        wkePaintUpdatedCallback _wkePaintUpdatedCallback;
        wkeDocumentReadyCallback _wkeDocumentReadyCallback;
        wkeLoadingFinishCallback _wkeLoadingFinishCallback;
        wkeDownloadFileCallback _wkeDownloadFileCallback;
        wkeCreateViewCallback _wkeCreateViewCallback;
        wkeLoadUrlEndCallback _wkeLoadUrlEndCallback;

        void OnwkeLoadUrlEndCallback(IntPtr webView, IntPtr param, string url, IntPtr job, IntPtr buf, int len)
        {
            if (OnUrlEndEvent != null) {
                byte[] managedArray = new byte[len];
                Marshal.Copy(buf, managedArray, 0, len);
                OnUrlEndEvent(managedArray, url, len);
            }
            //Console.WriteLine("call OnwkeLoadUrlEndCallback url:" + url); ;
        }

        IntPtr OnwkeCreateViewCallback(IntPtr webView, IntPtr param, wkeNavigationType navigationType, IntPtr url)
        {
            if (OnCreateViewEvent != null) {
                return OnCreateViewEvent(webView, param, navigationType, BlinkBrowserPInvoke.Utf8IntptrToString(BlinkBrowserPInvoke.wkeGetString(url)));
            } else {
                //Console.WriteLine("OnwkeCreateViewCallback url:" + BlinkBrowserPInvoke.Utf8IntptrToString(BlinkBrowserPInvoke.wkeGetString(url)));
                //Console.WriteLine("OnwkeCreateViewCallback navigationType:" + navigationType);
                return webView;
            }
        }
        bool OnwkeDownloadFileCallback(IntPtr webView, IntPtr param, string url)
        {
            Console.WriteLine("call OnwkeDownloadFileCallback:" + (url));
            if (OnDownloadFile != null) {
                OnDownloadFile(url);
            }
            return false;
        }

        void OnwkeLoadingFinishCallback(IntPtr webView, IntPtr param, IntPtr url, wkeLoadingResult result, IntPtr failedReason)
        {
            //Console.WriteLine("call OnwkeLoadingFinishCallback:" + wkeGetString(url).Utf8IntptrToString());
            //Console.WriteLine("call OnwkeLoadingFinishCallback result:" + result);

            if (result == wkeLoadingResult.WKE_LOADING_FAILED) {
                //Console.WriteLine("call OnwkeLoadingFinishCallback 加载失败 failedReason:" + BlinkBrowserPInvoke.Utf8IntptrToString(BlinkBrowserPInvoke.wkeGetString(failedReason)));
                HTML = "<h1>" + BlinkBrowserPInvoke.Utf8IntptrToString(BlinkBrowserPInvoke.wkeGetString(failedReason)) + "</h1>";
            } else {
                this.url = BlinkBrowserPInvoke.Utf8IntptrToString(BlinkBrowserPInvoke.wkeGetString(url));
                //Console.WriteLine("call OnwkeLoadingFinishCallback:成功加载完成。" + wkeGetString(url).Utf8IntptrToString());
            }
        }

        void OnwkeDocumentReadyCallback(IntPtr webView, IntPtr param)
        {
            //Console.WriteLine("call OnwkeDocumentReadyCallback:" + Marshal.PtrToStringUni(param));//.Utf8IntptrToString());
            //DocumentReadyCallback?.Invoke();
            if (DocumentReadyCallback != null) {
                DocumentReadyCallback();
            }
        }
        void OnwkeConsoleMessageCallback(IntPtr webView, IntPtr param, wkeConsoleLevel level, IntPtr message, IntPtr sourceName, int sourceLine, IntPtr stackTrace)
        {
            //Console.WriteLine("Console Msg:" + BlinkBrowserPInvoke.Utf8IntptrToString(BlinkBrowserPInvoke.wkeGetString(message)));
        }

        ContextMenuStrip contextMenuStrip = new ContextMenuStrip();
        public BlinkBrowser()
        {
            SetStyle(ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.DoubleBuffer |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.ResizeRedraw |
                //  ControlStyles.EnableNotifyMessage|
                ControlStyles.UserPaint, true);
            UpdateStyles();

            //contextMenuStrip.Opening += ContextMenuStrip1_Opening;

            ContextMenuStrip = contextMenuStrip;
            //ToolStripMenuItem tsmiGoBack = new ToolStripMenuItem("返回", null, (x, y) => {
            //    BlinkBrowserPInvoke.wkeGoBack(handle);
            //});
            //ToolStripMenuItem tsmiForward = new ToolStripMenuItem("前进", null, (x, y) => {
            //    BlinkBrowserPInvoke.wkeGoForward(handle);
            //});
            //ToolStripMenuItem tsmiReload = new ToolStripMenuItem("重新加载", null, (x, y) => {
            //    BlinkBrowserPInvoke.wkeReload(handle);
            //});
            //ToolStripMenuItem tsmiSelectAll = new ToolStripMenuItem("全选", null, (x, y) => {
            //    BlinkBrowserPInvoke.wkeSelectAll(handle);
            //});
            //ToolStripMenuItem tsmiCopy = new ToolStripMenuItem("复制", null, (x, y) => {
            //    BlinkBrowserPInvoke.wkeCopy(handle);
            //});
            //ToolStripMenuItem tsmiCut = new ToolStripMenuItem("剪切", null, (x, y) => {
            //    BlinkBrowserPInvoke.wkeCut(handle);
            //});
            //ToolStripMenuItem tsmiPaste = new ToolStripMenuItem("粘贴", null, (x, y) => {
            //    BlinkBrowserPInvoke.wkePaste(handle);
            //});
            //ToolStripMenuItem tsmiDelete = new ToolStripMenuItem("删除", null, (x, y) => {
            //    BlinkBrowserPInvoke.wkeDelete(handle);
            //});

            //contextMenuStrip.Items.Add(tsmiGoBack);
            //contextMenuStrip.Items.Add(tsmiForward);
            //contextMenuStrip.Items.Add(tsmiReload);
            //contextMenuStrip.Items.Add(tsmiSelectAll);
            //contextMenuStrip.Items.Add(tsmiCopy);
            //contextMenuStrip.Items.Add(tsmiCut);
            //contextMenuStrip.Items.Add(tsmiPaste);
            //contextMenuStrip.Items.Add(tsmiDelete);

            Application.AddMessageFilter(this);
        }

        ~BlinkBrowser()
        {
            if (handle != IntPtr.Zero) {
                BlinkBrowserPInvoke.wkeFinalize();
            }
        }

        void OnTitleChangedCallback(IntPtr webView, IntPtr param, IntPtr title)
        {
            if (OnTitleChangeCall != null) {
                OnTitleChangeCall(BlinkBrowserPInvoke.Utf8IntptrToString(BlinkBrowserPInvoke.wkeGetString(title)));
            }
        }

        void OnTitleChangedCallback2(IntPtr webView, IntPtr param, IntPtr title)
        {
            SetCursors();
            //Console.WriteLine("OnTitleChangedCallback2 title:" + MiniBlinkPinvoke.BlinkBrowserPInvoke.wkeGetString(title).Utf8IntptrToString());
        }

        bool OnwkeNavigationCallback(IntPtr webView, IntPtr param, wkeNavigationType navigationType, IntPtr url)
        {

            IntPtr urlPtr = BlinkBrowserPInvoke.wkeGetStringW(url);
            //Console.WriteLine(navigationType);

            //Console.WriteLine("OnwkeNavigationCallback:URL:" + Marshal.PtrToStringUni(urlPtr));

            if (OnUrlNavigationCall != null) {
                OnUrlNavigationCall(Marshal.PtrToStringUni(urlPtr));
            }

            return true;
        }

        void OnUrlChangedCallback(IntPtr webView, IntPtr param, IntPtr url)
        {
            if (OnUrlChangeCall != null) {
                OnUrlChangeCall(BlinkBrowserPInvoke.Utf8IntptrToString(BlinkBrowserPInvoke.wkeGetString(url)));
            }
            //OnUrlChangeCall?.Invoke(BlinkBrowserPInvoke.Utf8IntptrToString(BlinkBrowserPInvoke.wkeGetString(url)));
            //Console.WriteLine("OnUrlChangedCallback:URL:" +);
        }


        void OnWkePaintUpdatedCallback(IntPtr webView, IntPtr param, IntPtr hdc, int x, int y, int cx, int cy)
        {
            Invalidate();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            CreateCore();
        }

        /// <summary>
        /// 初始化 MB
        /// </summary>
        public void CreateCore()
        {
            BlinkBrowserPInvoke.wkeInitialize();
            handle = BlinkBrowserPInvoke.wkeCreateWebView();

            //只有开启才会触发 wkeOnCreateView
            BlinkBrowserPInvoke.wkeSetNavigationToNewWindowEnable(handle, true);

            BlinkBrowserPInvoke.wkeSetHandle(this.handle, this.Handle);
            BlinkBrowserPInvoke.wkeSetHandleOffset(handle, Location.X - 2, 0);

            //BlinkBrowserPInvoke.wkeSetTransparent(handle, true);
            BlinkBrowserPInvoke.wkeSetCookieEnabled(handle, true);
            if (string.IsNullOrEmpty(CookiePath)) {
                CookiePath = Path.Combine(Path.GetTempPath(), "MyBrowser\\cookie");
                if (!Directory.Exists(CookiePath)) { Directory.CreateDirectory(CookiePath); }
                BlinkBrowserPInvoke.wkeSetCookieJarPath(handle, CookiePath);


                LocalStoragePath = Path.Combine(Path.GetTempPath(), "MyBrowser\\LocalStorage");
                if (!Directory.Exists(LocalStoragePath)) { Directory.CreateDirectory(LocalStoragePath); }
                BlinkBrowserPInvoke.wkeSetLocalStorageFullPath(handle, LocalStoragePath);
            }

            BlinkBrowserPInvoke.wkeResize(handle, Width, Height);
            BlinkBrowserPInvoke.wkeSetUserAgentW(this.handle, Config.GetUserAgent());

            BindJsFunc();
            AlertBoxCallback = new AlertBoxCallback((a, b, c) => {
                MessageBox.Show(Marshal.PtrToStringUni(BlinkBrowserPInvoke.wkeToStringW(c)), Url + "提示");
            });
            BlinkBrowserPInvoke.wkeOnAlertBox(handle, AlertBoxCallback, IntPtr.Zero);

            //设置声音
            //BlinkBrowserPInvoke.wkeSetMediaVolume(handle, 20);

            _wkeNavigationCallback = OnwkeNavigationCallback;
            BlinkBrowserPInvoke.wkeOnNavigation(handle, _wkeNavigationCallback, IntPtr.Zero);


            titleChangeCallback = OnTitleChangedCallback;
            BlinkBrowserPInvoke.wkeOnTitleChanged(this.handle, titleChangeCallback, IntPtr.Zero);

            titleChangeCallback2 = OnTitleChangedCallback2;
            BlinkBrowserPInvoke.wkeOnMouseOverUrlChanged(this.handle, titleChangeCallback2, IntPtr.Zero);

            _wkeDocumentReadyCallback = OnwkeDocumentReadyCallback;
            BlinkBrowserPInvoke.wkeOnDocumentReady(this.handle, _wkeDocumentReadyCallback, IntPtr.Zero);

            urlChangedCallback = OnUrlChangedCallback;
            BlinkBrowserPInvoke.wkeOnURLChanged(this.handle, urlChangedCallback, IntPtr.Zero);

            _wkeConsoleMessageCallback = OnwkeConsoleMessageCallback;
            BlinkBrowserPInvoke.wkeOnConsole(this.handle, _wkeConsoleMessageCallback, IntPtr.Zero);

            _wkePaintUpdatedCallback = OnWkePaintUpdatedCallback;
            BlinkBrowserPInvoke.wkeOnPaintUpdated(this.handle, _wkePaintUpdatedCallback, IntPtr.Zero);

            _wkeLoadingFinishCallback = OnwkeLoadingFinishCallback;
            BlinkBrowserPInvoke.wkeOnLoadingFinish(this.handle, _wkeLoadingFinishCallback, IntPtr.Zero);

            //会导致 taobao 加载图片异常
            _wkeDownloadFileCallback = OnwkeDownloadFileCallback;
            BlinkBrowserPInvoke.wkeOnDownload(this.handle, _wkeDownloadFileCallback, IntPtr.Zero);

            _wkeCreateViewCallback = OnwkeCreateViewCallback;
            BlinkBrowserPInvoke.wkeOnCreateView(this.handle, _wkeCreateViewCallback, handle);

            _wkeLoadUrlEndCallback = OnwkeLoadUrlEndCallback;
            BlinkBrowserPInvoke.wkeOnLoadUrlEnd(this.handle, _wkeLoadUrlEndCallback, handle);

            //如果在设计器里赋值了，初始化后加载URL
            if (!string.IsNullOrEmpty(url)) {
                BlinkBrowserPInvoke.wkeLoadURLW(handle, url);
            }
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            if (handle != IntPtr.Zero) {
                using (var image = new Bitmap(ViewWidth, ViewHeight)) {
                    var bitmap = image.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                    BlinkBrowserPInvoke.wkePaint(handle, bitmap.Scan0, 0);
                    image.UnlockBits(bitmap);
                    e.Graphics.DrawImage(image, 0, 0);
                }


                //if (bits == IntPtr.Zero || oldSize != Size) {
                //    if (bits != IntPtr.Zero) {
                //        Marshal.FreeHGlobal(bits);
                //    }
                //    oldSize = Size;
                //    bits = Marshal.AllocHGlobal(Width * Height * 4);
                //}

                //BlinkBrowserPInvoke.wkePaint(handle, bits, 0);
                //using (Bitmap bmp = new Bitmap(Width, Height, Width * 4, PixelFormat.Format32bppPArgb, bits)) {
                //    e.Graphics.DrawImage(bmp, 0, 0);
                //}
            } else {
                base.OnPaint(e);
            }
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ViewWidth => BlinkBrowserPInvoke.wkeGetWidth(handle);

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int ViewHeight => BlinkBrowserPInvoke.wkeGetHeight(handle);
        public Bitmap DrawToBitmap(Rectangle? rect = null)
        {
            if (rect.HasValue == false) {
                rect = new Rectangle(0, 0, ViewWidth, ViewHeight);
            }

            using (var image = new Bitmap(ViewWidth, ViewHeight)) {
                var bitmap = image.LockBits(new Rectangle(0, 0, image.Width, image.Height),
                    ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                BlinkBrowserPInvoke.wkePaint(handle, bitmap.Scan0, 0);
                image.UnlockBits(bitmap);
                return image.Clone(rect.Value, PixelFormat.Format32bppArgb);
            }
        }


        #region 鼠标和按键


        void SetCursors()
        {
            switch (BlinkBrowserPInvoke.wkeGetCursorInfoType(handle)) {
                case WkeCursorInfo.WkeCursorInfoPointer:
                    Cursor = Cursors.Default;
                    break;
                case WkeCursorInfo.WkeCursorInfoCross:
                    Cursor = Cursors.Cross;
                    break;
                case WkeCursorInfo.WkeCursorInfoHand:
                    Cursor = Cursors.Hand;
                    break;
                case WkeCursorInfo.WkeCursorInfoIBeam:
                    Cursor = Cursors.IBeam;
                    break;
                case WkeCursorInfo.WkeCursorInfoWait:
                    Cursor = Cursors.WaitCursor;
                    break;
                case WkeCursorInfo.WkeCursorInfoHelp:
                    Cursor = Cursors.Help;
                    break;
                case WkeCursorInfo.WkeCursorInfoEastResize:
                    Cursor = Cursors.SizeWE;
                    break;
                case WkeCursorInfo.WkeCursorInfoNorthResize:
                    Cursor = Cursors.SizeNS;
                    break;
                case WkeCursorInfo.WkeCursorInfoNorthEastResize:
                    Cursor = Cursors.SizeNESW;
                    break;
                case WkeCursorInfo.WkeCursorInfoNorthWestResize:
                    Cursor = Cursors.SizeNWSE;
                    break;
                case WkeCursorInfo.WkeCursorInfoSouthResize:
                    Cursor = Cursors.SizeWE;
                    break;
                case WkeCursorInfo.WkeCursorInfoSouthEastResize:
                    Cursor = Cursors.SizeNWSE;
                    break;
                case WkeCursorInfo.WkeCursorInfoSouthWestResize:
                    Cursor = Cursors.SizeNESW;
                    break;
                case WkeCursorInfo.WkeCursorInfoWestResize:
                    Cursor = Cursors.SizeWE;
                    break;
                case WkeCursorInfo.WkeCursorInfoNorthSouthResize:
                    Cursor = Cursors.SizeNS;
                    break;
                case WkeCursorInfo.WkeCursorInfoEastWestResize:
                    Cursor = Cursors.SizeWE;
                    break;
                case WkeCursorInfo.WkeCursorInfoNorthEastSouthWestResize:
                    Cursor = Cursors.SizeAll;
                    break;
                case WkeCursorInfo.WkeCursorInfoNorthWestSouthEastResize:
                    Cursor = Cursors.SizeAll;
                    break;
                case WkeCursorInfo.WkeCursorInfoColumnResize:
                    Cursor = Cursors.Default;
                    break;
                case WkeCursorInfo.WkeCursorInfoRowResize:
                    Cursor = Cursors.Default;
                    break;
                default:
                    Cursor = Cursors.Default;
                    break;
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (handle != IntPtr.Zero && Width > 0 && Height > 0) {
                BlinkBrowserPInvoke.wkeResize(handle, Width, Height);
                Invalidate();
            }
        }
        protected override void OnMouseWheel(MouseEventArgs e)
        {

            base.OnMouseWheel(e);
            if (handle != IntPtr.Zero) {
                uint flags = GetMouseFlags(e);
                BlinkBrowserPInvoke.wkeFireMouseWheelEvent(handle, e.X, e.Y, e.Delta, flags);
            }
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {

            base.OnMouseDown(e);
            uint msg = 0;
            if (e.Button == MouseButtons.Left) {
                msg = (uint)wkeMouseMessage.WKE_MSG_LBUTTONDOWN;
            } else if (e.Button == MouseButtons.Middle) {
                msg = (uint)wkeMouseMessage.WKE_MSG_MBUTTONDOWN;
            } else if (e.Button == MouseButtons.Right) {
                msg = (uint)wkeMouseMessage.WKE_MSG_RBUTTONDOWN;
            }
            uint flags = GetMouseFlags(e);
            if (handle != IntPtr.Zero) {
                BlinkBrowserPInvoke.wkeFireMouseEvent(handle, msg, e.X, e.Y, flags);
            }
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            uint msg = 0;
            if (e.Button == MouseButtons.Left) {
                msg = (uint)wkeMouseMessage.WKE_MSG_LBUTTONUP;
            } else if (e.Button == MouseButtons.Middle) {
                msg = (uint)wkeMouseMessage.WKE_MSG_MBUTTONUP;
            } else if (e.Button == MouseButtons.Right) {
                msg = (uint)wkeMouseMessage.WKE_MSG_RBUTTONUP;
            }
            uint flags = GetMouseFlags(e);
            if (handle != IntPtr.Zero) {
                BlinkBrowserPInvoke.wkeFireMouseEvent(handle, msg, e.X, e.Y, flags);
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            //base.OnMouseMove(e);
            //if (handle != IntPtr.Zero)
            //{
            //    //uint msg = (uint)wkeMouseMessage.WKE_MSG_MOUSEMOVE;
            //    uint flags = GetMouseFlags(e);
            //    //EwePInvoke.wkeFireMouseEvent(handle, msg, e.X, e.Y, flags);
            //    EwePInvoke.wkeFireMouseEvent(handle, 0x200, e.X, e.Y, flags);
            //}
            base.OnMouseMove(e);
            if (this.handle != IntPtr.Zero) {
                uint flags = GetMouseFlags(e);
                BlinkBrowserPInvoke.wkeFireMouseEvent(this.handle, 0x200, e.X, e.Y, flags);
            }
        }
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (handle != IntPtr.Zero) {
                BlinkBrowserPInvoke.wkeFireKeyDownEvent(handle, (uint)e.KeyValue, 0, false);
            }
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            if (handle != IntPtr.Zero) {
                e.Handled = true;
                BlinkBrowserPInvoke.wkeFireKeyPressEvent(handle, (uint)e.KeyChar, 0, false);
            }
        }
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (handle != IntPtr.Zero) {
                BlinkBrowserPInvoke.wkeFireKeyUpEvent(handle, (uint)e.KeyValue, 0, false);
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            if (handle != IntPtr.Zero) {
                BlinkBrowserPInvoke.wkeSetFocus(handle);
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            if (handle != IntPtr.Zero) {
                BlinkBrowserPInvoke.wkeKillFocus(handle);
            }
        }

        protected override void OnPreviewKeyDown(PreviewKeyDownEventArgs e)
        {
            base.OnPreviewKeyDown(e);
            switch (e.KeyCode) {
                case Keys.Down:
                case Keys.Up:
                case Keys.Left:
                case Keys.Right:
                case Keys.Tab:
                    e.IsInputKey = true;
                    break;
            }
        }
        private static uint GetMouseFlags(MouseEventArgs e)
        {
            uint flags = 0;
            if (e.Button == MouseButtons.Left) {
                flags = flags | (uint)wkeMouseFlags.WKE_LBUTTON;
            }
            if (e.Button == MouseButtons.Middle) {
                flags = flags | (uint)wkeMouseFlags.WKE_MBUTTON;
            }
            if (e.Button == MouseButtons.Right) {
                flags = flags | (uint)wkeMouseFlags.WKE_RBUTTON;
            }
            if (Control.ModifierKeys == Keys.Control) {
                flags = flags | (uint)wkeMouseFlags.WKE_CONTROL;
            }
            if (Control.ModifierKeys == Keys.Shift) {
                flags = flags | (uint)wkeMouseFlags.WKE_SHIFT;
            }
            return flags;
        }
        #endregion

        /// <summary>
        /// 执行js
        /// </summary>
        /// <param name="js"></param>
        /// <returns></returns>
        public JsValue InvokeJS(string js)
        {
            IntPtr jsPtr = Marshal.StringToCoTaskMemAnsi(js);
            JsValue result = new JsValue(BlinkBrowserPInvoke.wkeRunJS(handle, jsPtr), BlinkBrowserPInvoke.wkeGlobalExec(handle));
            Marshal.FreeCoTaskMem(jsPtr);
            return result;
        }


        /// <summary>
        /// 执行js
        /// </summary>
        /// <param name="js"></param>
        /// <returns></returns>
        public JsValue InvokeJSW(string js)
        {
            return new JsValue(BlinkBrowserPInvoke.wkeRunJSW(handle, js), BlinkBrowserPInvoke.wkeGlobalExec(handle));
        }

        /// <summary>
        /// 绑定带有 JSFunctin 属性的方法到前台JS，实现前后台相互调用。
        /// </summary>
        public void BindJsFunc()
        {
            if (GlobalObjectJs == null) {
                GlobalObjectJs = this;
            }
            var att = GlobalObjectJs.GetType().GetMethods();
            //jsnaviteList.Clear();
            var result = new ArrayList();
            foreach (var item in att) {
                var xx = item.GetCustomAttributes(typeof(JSFunc), true);
                if (xx != null && xx.Length != 0) {
                    var jsnav = new wkeJsNativeFunction((es, _param) => {
                        var xp = item.GetParameters();
                        var argcount = BlinkBrowserPInvoke.jsArgCount(es);
                        long param = 0L;
                        if (xp != null && xp.Length != 0 && argcount != 0) {

                            object[] listParam = new object[BlinkBrowserPInvoke.jsArgCount(es)];
                            for (int i = 0; i < argcount; i++) {
                                Type tType = xp[i].ParameterType;

                                var paramnow = BlinkBrowserPInvoke.jsArg(es, i);
                                param = paramnow;
                                if (tType == typeof(int)) {
                                    listParam[i] = Convert.ChangeType(BlinkBrowserPInvoke.jsToInt(es, paramnow), tType);
                                } else
                                if (tType == typeof(double)) {
                                    listParam[i] = Convert.ChangeType(BlinkBrowserPInvoke.jsToDouble(es, paramnow), tType);
                                } else
                                if (tType == typeof(float)) {
                                    listParam[i] = Convert.ChangeType(BlinkBrowserPInvoke.jsToFloat(es, paramnow), tType);
                                } else
                                if (tType == typeof(bool)) {
                                    listParam[i] = Convert.ChangeType(BlinkBrowserPInvoke.jsToBoolean(es, paramnow), tType);
                                } else {
                                    listParam[i] = Convert.ChangeType((BlinkBrowserPInvoke.Utf8IntptrToString(BlinkBrowserPInvoke.jsToString(es, paramnow))), tType);
                                }
                            }
                            try {
                                var res = item.Invoke(GlobalObjectJs, listParam);
                                if (res != null) {
                                    var mStr = Marshal.StringToHGlobalUni(res.ToString());
                                    return BlinkBrowserPInvoke.jsStringW(es, mStr);//返回JS字符串
                                }
                            } catch (Exception ex) {
                                Console.WriteLine(ex.ToString());
                            }
                        } else {
                            var res = item.Invoke(GlobalObjectJs, null);
                            if (res != null) {
                                var mStr = Marshal.StringToHGlobalUni(res.ToString());
                                return BlinkBrowserPInvoke.jsStringW(es, mStr);//返回JS字符串
                            }
                        }
                        return param;
                    });
                    var name = (xx[0] as JSFunc).Name;
                    if (string.IsNullOrEmpty(name)) {
                        name = item.Name;
                    }
                    BlinkBrowserPInvoke.wkeJsBindFunction(name, jsnav, IntPtr.Zero, (uint)item.GetParameters().Length);
                    listObj.Add(jsnav);
                }
            }
        }

        public float _ZoomFactor { get; set; }
        [Browsable(false), DefaultValue(1)]
        public float ZoomFactor {
            get {
                if (this.handle == IntPtr.Zero) {
                    return this._ZoomFactor;
                }
                return MiniBlinkPinvoke.BlinkBrowserPInvoke.wkeGetZoomFactor(this.handle);
            }
            set {
                this._ZoomFactor = value;
                if (this.handle != IntPtr.Zero) {
                    MiniBlinkPinvoke.BlinkBrowserPInvoke.wkeSetZoomFactor(this.handle, value);
                }
            }
        }

        #region JSFunc
        [JSFunc("GetMAC")]
        public string GetMAC()
        {
            return "";
        }
        [JSFunc("GetMachineCode")]
        public string GetMachineCode()
        {
            return "";
        }
        [JSFunc("OpenUrl")]
        public void OpenUrl(object url)
        {
        }

        #endregion


        public bool PreFilterMessage(ref Message m)
        {
            IntPtr myPtr = BlinkBrowserPInvoke.GetForegroundWindow();

            int length = BlinkBrowserPInvoke.GetWindowTextLength(myPtr);
            StringBuilder windowName = new StringBuilder(length + 1);
            BlinkBrowserPInvoke.GetWindowText(myPtr, windowName, windowName.Capacity);
            if (windowName.ToString() == "Miniblink Devtools") {
                int wp = m.WParam.ToInt32();
                if (wp == 13/*回车*/ || wp == 37/*左 */ || wp == 39/*右*/ || wp == 38/*上*/ || wp == 40/*下*/) {
                    BlinkBrowserPInvoke.SendMessage(m.HWnd, m.Msg, m.WParam, m.LParam);
                }
                if (m.Msg == 258)//字符
                {
                    BlinkBrowserPInvoke.SendMessage(m.HWnd, m.Msg, m.WParam, m.LParam);
                }
            }
            return false;
        }

        /// <summary>
        /// 解析 cookies.dat文件得到Cookie,没有判断 path，只有 域的判断
        /// </summary>
        public string GetCookiesFromFile {
            get {
                StringBuilder sbCookie = new StringBuilder();
                if (File.Exists(CookiePath + "\\cookies.dat")) {

                    var uri = new Uri(Url);
                    string host;// = uri.Host;

                    var allCookies = File.ReadAllLines(CookiePath + "\\cookies.dat");
                    for (int i = 4; i < allCookies.Length; i++) {
                        host = uri.Host;
                        var listCookie = allCookies[i].Split('\t');
                        if (listCookie != null && listCookie.Length != 0 && listCookie.Length == 7) {
                            var _cookie = listCookie[0];

                            Lable:
                            if (_cookie == host) {
                                sbCookie.AppendFormat("{0}={1};", listCookie[5], listCookie[6]);
                            }
                            //httponly
                            var httpOnly = "#HttpOnly_" + host;
                            if (_cookie == httpOnly) {
                                sbCookie.AppendFormat("{0}={1};", listCookie[5], listCookie[6]);
                            }
                            if (host.IndexOf('.') == 0)// . 开头
                            {
                                host = host.Substring(host.IndexOf('.') + 1);//. 开头 去掉 .
                                goto Lable;
                            } else {
                                if (host.TrimStart('.').Split('.').Length > 2) {
                                    host = host.Substring(host.IndexOf('.'));//带 . 
                                    goto Lable;
                                }
                            }
                        }

                    }
                }
                return sbCookie.ToString();
            }
        }

        /// <summary>
        /// 获取或设置网页源码
        /// </summary>
        public string HTML {
            get {
                if (handle != IntPtr.Zero) {
                    return InvokeJSW("return document.documentElement.outerHTML").ToString();
                } else {
                    return string.Empty;
                }
            }
            set {
                if (handle != IntPtr.Zero) {
                    BlinkBrowserPInvoke.wkeLoadHTMLW(this.handle, value);
                }
            }
        }
        public string Url {
            get { return url; }
            set {
                url = value;
                if (handle != IntPtr.Zero) {
                    BlinkBrowserPInvoke.wkeLoadURLW(handle, url);
                }
            }
        }

        public string Cookies {
            get {
                if (handle != IntPtr.Zero) {
                    return BlinkBrowserPInvoke.Utf8IntptrToString(BlinkBrowserPInvoke.wkeGetCookie(handle));
                } else {
                    return string.Empty;
                }
            }
        }
    }
}
