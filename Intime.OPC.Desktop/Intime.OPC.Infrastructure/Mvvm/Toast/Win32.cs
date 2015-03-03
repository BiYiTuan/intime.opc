using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using Microsoft.Win32;
using System.Runtime.CompilerServices;

namespace Intime.OPC.Infrastructure.Mvvm.Toast
{
    public class Win32
    {
        #region Structs and Types declarations

        [StructLayout(LayoutKind.Sequential)]
        public struct STAT_WORKSTATION_0
        {
            [MarshalAs(UnmanagedType.I8)]
            public Int64 StatisticsStartTime;

            public long BytesReceived;
            public long SmbsReceived;
            public long PagingReadBytesRequested;
            public long NonPagingReadBytesRequested;
            public long CacheReadBytesRequested;
            public long NetworkReadBytesRequested;

            public long BytesTransmitted;
            public long SmbsTransmitted;
            public long PagingWriteBytesRequested;
            public long NonPagingWriteBytesRequested;
            public long CacheWriteBytesRequested;
            public long NetworkWriteBytesRequested;

            public int InitiallyFailedOperations;
            public int FailedCompletionOperations;

            public int ReadOperations;
            public int RandomReadOperations;
            public int ReadSmbs;
            public int LargeReadSmbs;
            public int SmallReadSmbs;

            public int WriteOperations;
            public int RandomWriteOperations;
            public int WriteSmbs;
            public int LargeWriteSmbs;
            public int SmallWriteSmbs;

            public int RawReadsDenied;
            public int RawWritesDenied;

            public int NetworkErrors;

            //  Connection/Session counts
            public int Sessions;
            public int FailedSessions;
            public int Reconnects;
            public int CoreConnects;
            public int Lanman20Connects;
            public int Lanman21Connects;
            public int LanmanNtConnects;
            public int ServerDisconnects;
            public int HungSessions;
            public int UseCount;
            public int FailedUseCount;

            public int CurrentCommands;
        }

        public enum WindowsMessages : int
        {
            WM_ACTIVATE = 0x6,
            WM_ACTIVATEAPP = 0x1C,
            WM_AFXFIRST = 0x360,
            WM_AFXLAST = 0x37F,
            WM_APP = 0x8000,
            WM_ASKCBFORMATNAME = 0x30C,
            WM_CANCELJOURNAL = 0x4B,
            WM_CANCELMODE = 0x1F,
            WM_CAPTURECHANGED = 0x215,
            WM_CHANGECBCHAIN = 0x30D,
            WM_CHAR = 0x102,
            WM_CHARTOITEM = 0x2F,
            WM_CHILDACTIVATE = 0x22,
            WM_CLEAR = 0x303,
            WM_CLOSE = 0x10,
            WM_COMMAND = 0x111,
            WM_COMPACTING = 0x41,
            WM_COMPAREITEM = 0x39,
            WM_CONTEXTMENU = 0x7B,
            WM_COPY = 0x301,
            WM_COPYDATA = 0x4A,
            WM_CREATE = 0x1,
            WM_CTLCOLORBTN = 0x135,
            WM_CTLCOLORDLG = 0x136,
            WM_CTLCOLOREDIT = 0x133,
            WM_CTLCOLORLISTBOX = 0x134,
            WM_CTLCOLORMSGBOX = 0x132,
            WM_CTLCOLORSCROLLBAR = 0x137,
            WM_CTLCOLORSTATIC = 0x138,
            WM_CUT = 0x300,
            WM_DEADCHAR = 0x103,
            WM_DELETEITEM = 0x2D,
            WM_DESTROY = 0x2,
            WM_DESTROYCLIPBOARD = 0x307,
            WM_DEVICECHANGE = 0x219,
            WM_DEVMODECHANGE = 0x1B,
            WM_DISPLAYCHANGE = 0x7E,
            WM_DRAWCLIPBOARD = 0x308,
            WM_DRAWITEM = 0x2B,
            WM_DROPFILES = 0x233,
            WM_ENABLE = 0xA,
            WM_ENDSESSION = 0x16,
            WM_ENTERIDLE = 0x121,
            WM_ENTERMENULOOP = 0x211,
            WM_ENTERSIZEMOVE = 0x231,
            WM_ERASEBKGND = 0x14,
            WM_EXITMENULOOP = 0x212,
            WM_EXITSIZEMOVE = 0x232,
            WM_FONTCHANGE = 0x1D,
            WM_GETDLGCODE = 0x87,
            WM_GETFONT = 0x31,
            WM_GETHOTKEY = 0x33,
            WM_GETICON = 0x7F,
            WM_GETMINMAXINFO = 0x24,
            WM_GETOBJECT = 0x3D,
            WM_GETSYSMENU = 0x313,
            WM_GETTEXT = 0xD,
            WM_GETTEXTLENGTH = 0xE,
            WM_HANDHELDFIRST = 0x358,
            WM_HANDHELDLAST = 0x35F,
            WM_HELP = 0x53,
            WM_HOTKEY = 0x312,
            WM_HSCROLL = 0x114,
            WM_HSCROLLCLIPBOARD = 0x30E,
            WM_ICONERASEBKGND = 0x27,
            WM_IME_CHAR = 0x286,
            WM_IME_COMPOSITION = 0x10F,
            WM_IME_COMPOSITIONFULL = 0x284,
            WM_IME_CONTROL = 0x283,
            WM_IME_ENDCOMPOSITION = 0x10E,
            WM_IME_KEYDOWN = 0x290,
            WM_IME_KEYLAST = 0x10F,
            WM_IME_KEYUP = 0x291,
            WM_IME_NOTIFY = 0x282,
            WM_IME_REQUEST = 0x288,
            WM_IME_SELECT = 0x285,
            WM_IME_SETCONTEXT = 0x281,
            WM_IME_STARTCOMPOSITION = 0x10D,
            WM_INITDIALOG = 0x110,
            WM_INITMENU = 0x116,
            WM_INITMENUPOPUP = 0x117,
            WM_INPUTLANGCHANGE = 0x51,
            WM_INPUTLANGCHANGEREQUEST = 0x50,
            WM_KEYDOWN = 0x100,
            WM_KEYFIRST = 0x100,
            WM_KEYLAST = 0x108,
            WM_KEYUP = 0x101,
            WM_KILLFOCUS = 0x8,
            WM_LBUTTONDBLCLK = 0x203,
            WM_LBUTTONDOWN = 0x201,
            WM_LBUTTONUP = 0x202,
            WM_MBUTTONDBLCLK = 0x209,
            WM_MBUTTONDOWN = 0x207,
            WM_MBUTTONUP = 0x208,
            WM_MDIACTIVATE = 0x222,
            WM_MDICASCADE = 0x227,
            WM_MDICREATE = 0x220,
            WM_MDIDESTROY = 0x221,
            WM_MDIGETACTIVE = 0x229,
            WM_MDIICONARRANGE = 0x228,
            WM_MDIMAXIMIZE = 0x225,
            WM_MDINEXT = 0x224,
            WM_MDIREFRESHMENU = 0x234,
            WM_MDIRESTORE = 0x223,
            WM_MDISETMENU = 0x230,
            WM_MDITILE = 0x226,
            WM_MEASUREITEM = 0x2C,
            WM_MENUCHAR = 0x120,
            WM_MENUCOMMAND = 0x126,
            WM_MENUDRAG = 0x123,
            WM_MENUGETOBJECT = 0x124,
            WM_MENURBUTTONUP = 0x122,
            WM_MENUSELECT = 0x11F,
            WM_MOUSEACTIVATE = 0x21,
            WM_MOUSEFIRST = 0x200,
            WM_MOUSEHOVER = 0x2A1,
            WM_MOUSELAST = 0x20A,
            WM_MOUSELEAVE = 0x2A3,
            WM_MOUSEMOVE = 0x200,
            WM_MOUSEWHEEL = 0x20A,
            WM_MOVE = 0x3,
            WM_MOVING = 0x216,
            WM_NCACTIVATE = 0x86,
            WM_NCCALCSIZE = 0x83,
            WM_NCCREATE = 0x81,
            WM_NCDESTROY = 0x82,
            WM_NCHITTEST = 0x84,
            WM_NCLBUTTONDBLCLK = 0xA3,
            WM_NCLBUTTONDOWN = 0xA1,
            WM_NCLBUTTONUP = 0xA2,
            WM_NCMBUTTONDBLCLK = 0xA9,
            WM_NCMBUTTONDOWN = 0xA7,
            WM_NCMBUTTONUP = 0xA8,
            WM_NCMOUSEHOVER = 0x2A0,
            WM_NCMOUSELEAVE = 0x2A2,
            WM_NCMOUSEMOVE = 0xA0,
            WM_NCPAINT = 0x85,
            WM_NCRBUTTONDBLCLK = 0xA6,
            WM_NCRBUTTONDOWN = 0xA4,
            WM_NCRBUTTONUP = 0xA5,
            WM_NEXTDLGCTL = 0x28,
            WM_NEXTMENU = 0x213,
            WM_NOTIFY = 0x4E,
            WM_NOTIFYFORMAT = 0x55,
            WM_NULL = 0x0,
            WM_PAINT = 0xF,
            WM_PAINTCLIPBOARD = 0x309,
            WM_PAINTICON = 0x26,
            WM_PALETTECHANGED = 0x311,
            WM_PALETTEISCHANGING = 0x310,
            WM_PARENTNOTIFY = 0x210,
            WM_PASTE = 0x302,
            WM_PENWINFIRST = 0x380,
            WM_PENWINLAST = 0x38F,
            WM_POWER = 0x48,
            WM_PRINT = 0x317,
            WM_PRINTCLIENT = 0x318,
            WM_QUERYDRAGICON = 0x37,
            WM_QUERYENDSESSION = 0x11,
            WM_QUERYNEWPALETTE = 0x30F,
            WM_QUERYOPEN = 0x13,
            WM_QUERYUISTATE = 0x129,
            WM_QUEUESYNC = 0x23,
            WM_QUIT = 0x12,
            WM_RBUTTONDBLCLK = 0x206,
            WM_RBUTTONDOWN = 0x204,
            WM_RBUTTONUP = 0x205,
            WM_RENDERALLFORMATS = 0x306,
            WM_RENDERFORMAT = 0x305,
            WM_SETCURSOR = 0x20,
            WM_SETFOCUS = 0x7,
            WM_SETFONT = 0x30,
            WM_SETHOTKEY = 0x32,
            WM_SETICON = 0x80,
            WM_SETREDRAW = 0xB,
            WM_SETTEXT = 0xC,
            WM_SETTINGCHANGE = 0x1A,
            WM_SHOWWINDOW = 0x18,
            WM_SIZE = 0x5,
            WM_SIZECLIPBOARD = 0x30B,
            WM_SIZING = 0x214,
            WM_SPOOLERSTATUS = 0x2A,
            WM_STYLECHANGED = 0x7D,
            WM_STYLECHANGING = 0x7C,
            WM_SYNCPAINT = 0x88,
            WM_SYSCHAR = 0x106,
            WM_SYSCOLORCHANGE = 0x15,
            WM_SYSCOMMAND = 0x112,
            WM_SYSDEADCHAR = 0x107,
            WM_SYSKEYDOWN = 0x104,
            WM_SYSKEYUP = 0x105,
            WM_SYSTIMER = 0x118,  // undocumented, see http://support.microsoft.com/?id=108938
            WM_TCARD = 0x52,
            WM_TIMECHANGE = 0x1E,
            WM_TIMER = 0x113,
            WM_UNDO = 0x304,
            WM_UNINITMENUPOPUP = 0x125,
            WM_USER = 0x400,
            WM_USERCHANGED = 0x54,
            WM_VKEYTOITEM = 0x2E,
            WM_VSCROLL = 0x115,
            WM_VSCROLLCLIPBOARD = 0x30A,
            WM_WINDOWPOSCHANGED = 0x47,
            WM_WINDOWPOSCHANGING = 0x46,
            WM_WININICHANGE = 0x1A,
            WM_XBUTTONDBLCLK = 0x20D,
            WM_XBUTTONDOWN = 0x20B,
            WM_XBUTTONUP = 0x20C
        }

        // Some HRESULTs
        public enum HRESULTS : uint
        {
            S_OK = 0,
            S_FALSE = 1,

            E_NOTIMPL = 0x80004001,
            E_OUTOFMEMORY = 0x8007000E,
            E_INVALIDARG = 0x80070057,
            E_NOINTERFACE = 0x80004002,
            E_POINTER = 0x80004003,
            E_HANDLE = 0x80070006,
            E_ABORT = 0x80004004,
            E_FAIL = 0x80004005,
            E_ACCESSDENIED = 0x80070005,

            // IConnectionPoint errors

            CONNECT_E_FIRST = 0x80040200,
            CONNECT_E_NOCONNECTION,  // there is no connection for this connection id
            CONNECT_E_ADVISELIMIT,   // this implementation's limit for advisory connections has been reached
            CONNECT_E_CANNOTCONNECT, // connection attempt failed
            CONNECT_E_OVERRIDDEN,    // must use a derived interface to connect

            // DllRegisterServer/DllUnregisterServer errors
            SELFREG_E_TYPELIB = 0x80040200, // failed to register/unregister type library
            SELFREG_E_CLASS,        // failed to register/unregister class

            // INET errors

            INET_E_INVALID_URL = 0x800C0002,
            INET_E_NO_SESSION = 0x800C0003,
            INET_E_CANNOT_CONNECT = 0x800C0004,
            INET_E_RESOURCE_NOT_FOUND = 0x800C0005,
            INET_E_OBJECT_NOT_FOUND = 0x800C0006,
            INET_E_DATA_NOT_AVAILABLE = 0x800C0007,
            INET_E_DOWNLOAD_FAILURE = 0x800C0008,
            INET_E_AUTHENTICATION_REQUIRED = 0x800C0009,
            INET_E_NO_VALID_MEDIA = 0x800C000A,
            INET_E_CONNECTION_TIMEOUT = 0x800C000B,
            INET_E_INVALID_REQUEST = 0x800C000C,
            INET_E_UNKNOWN_PROTOCOL = 0x800C000D,
            INET_E_SECURITY_PROBLEM = 0x800C000E,
            INET_E_CANNOT_LOAD_DATA = 0x800C000F,
            INET_E_CANNOT_INSTANTIATE_OBJECT = 0x800C0010,
            INET_E_USE_DEFAULT_PROTOCOLHANDLER = 0x800C0011,
            INET_E_DEFAULT_ACTION = 0x800C0011,
            INET_E_USE_DEFAULT_SETTING = 0x800C0012,
            INET_E_QUERYOPTION_UNKNOWN = 0x800C0013,
            INET_E_REDIRECT_FAILED = 0x800C0014,//INET_E_REDIRECTING 
            INET_E_REDIRECT_TO_DIR = 0x800C0015,
            INET_E_CANNOT_LOCK_REQUEST = 0x800C0016,
            INET_E_USE_EXTEND_BINDING = 0x800C0017,
            INET_E_ERROR_FIRST = 0x800C0002,
            INET_E_ERROR_LAST = 0x800C0017,
            INET_E_CODE_DOWNLOAD_DECLINED = 0x800C0100,
            INET_E_RESULT_DISPATCHED = 0x800C0200,
            INET_E_CANNOT_REPLACE_SFP_FILE = 0x800C0300,

        }

        public enum WinInetErrors
        {
            HTTP_STATUS_CONTINUE = 100, //The request can be continued.
            HTTP_STATUS_SWITCH_PROTOCOLS = 101, //The server has switched protocols in an upgrade header.
            HTTP_STATUS_OK = 200, //The request completed successfully.
            HTTP_STATUS_CREATED = 201, //The request has been fulfilled and resulted in the creation of a new resource.
            HTTP_STATUS_ACCEPTED = 202, //The request has been accepted for processing, but the processing has not been completed.
            HTTP_STATUS_PARTIAL = 203, //The returned meta information in the entity-header is not the definitive set available from the origin server.
            HTTP_STATUS_NO_CONTENT = 204, //The server has fulfilled the request, but there is no new information to send back.
            HTTP_STATUS_RESET_CONTENT = 205, //The request has been completed, and the client program should reset the document view that caused the request to be sent to allow the user to easily initiate another input action.
            HTTP_STATUS_PARTIAL_CONTENT = 206, //The server has fulfilled the partial GET request for the resource.
            HTTP_STATUS_AMBIGUOUS = 300, //The server couldn't decide what to return.
            HTTP_STATUS_MOVED = 301, //The requested resource has been assigned to a new permanent URI (Uniform Resource Identifier), and any future references to this resource should be done using one of the returned URIs.
            HTTP_STATUS_REDIRECT = 302, //The requested resource resides temporarily under a different URI (Uniform Resource Identifier).
            HTTP_STATUS_REDIRECT_METHOD = 303, //The response to the request can be found under a different URI (Uniform Resource Identifier) and should be retrieved using a GET HTTP verb on that resource.
            HTTP_STATUS_NOT_MODIFIED = 304, //The requested resource has not been modified.
            HTTP_STATUS_USE_PROXY = 305, //The requested resource must be accessed through the proxy given by the location field.
            HTTP_STATUS_REDIRECT_KEEP_VERB = 307, //The redirected request keeps the same HTTP verb. HTTP/1.1 behavior.

            HTTP_STATUS_BAD_REQUEST = 400,
            HTTP_STATUS_DENIED = 401,
            HTTP_STATUS_PAYMENT_REQ = 402,
            HTTP_STATUS_FORBIDDEN = 403,
            HTTP_STATUS_NOT_FOUND = 404,
            HTTP_STATUS_BAD_METHOD = 405,
            HTTP_STATUS_NONE_ACCEPTABLE = 406,
            HTTP_STATUS_PROXY_AUTH_REQ = 407,
            HTTP_STATUS_REQUEST_TIMEOUT = 408,
            HTTP_STATUS_CONFLICT = 409,
            HTTP_STATUS_GONE = 410,
            HTTP_STATUS_LENGTH_REQUIRED = 411,
            HTTP_STATUS_PRECOND_FAILED = 412,
            HTTP_STATUS_REQUEST_TOO_LARGE = 413,
            HTTP_STATUS_URI_TOO_LONG = 414,
            HTTP_STATUS_UNSUPPORTED_MEDIA = 415,
            HTTP_STATUS_RETRY_WITH = 449,
            HTTP_STATUS_SERVER_ERROR = 500,
            HTTP_STATUS_NOT_SUPPORTED = 501,
            HTTP_STATUS_BAD_GATEWAY = 502,
            HTTP_STATUS_SERVICE_UNAVAIL = 503,
            HTTP_STATUS_GATEWAY_TIMEOUT = 504,
            HTTP_STATUS_VERSION_NOT_SUP = 505,

            ERROR_INTERNET_ASYNC_THREAD_FAILED = 12047,    //The application could not start an asynchronous thread.
            ERROR_INTERNET_BAD_AUTO_PROXY_SCRIPT = 12166,    //There was an error in the automatic proxy configuration script.
            ERROR_INTERNET_BAD_OPTION_LENGTH = 12010,    //The length of an option supplied to InternetQueryOption or InternetSetOption is incorrect for the type of option specified.
            ERROR_INTERNET_BAD_REGISTRY_PARAMETER = 12022,    //A required registry value was located but is an incorrect type or has an invalid value.
            ERROR_INTERNET_CANNOT_CONNECT = 12029,    //The attempt to connect to the server failed.
            ERROR_INTERNET_CHG_POST_IS_NON_SECURE = 12042,    //The application is posting and attempting to change multiple lines of text on a server that is not secure.
            ERROR_INTERNET_CLIENT_AUTH_CERT_NEEDED = 12044,    //The server is requesting client authentication.
            ERROR_INTERNET_CLIENT_AUTH_NOT_SETUP = 12046,    //Client authorization is not set up on this computer.
            ERROR_INTERNET_CONNECTION_ABORTED = 12030,    //The connection with the server has been terminated.
            ERROR_INTERNET_CONNECTION_RESET = 12031,    //The connection with the server has been reset.
            ERROR_INTERNET_DIALOG_PENDING = 12049,    //Another thread has a password dialog box in progress.
            ERROR_INTERNET_DISCONNECTED = 12163,    //The Internet connection has been lost.
            ERROR_INTERNET_EXTENDED_ERROR = 12003,    //An extended error was returned from the server. This is typically a string or buffer containing a verbose error message. Call InternetGetLastResponseInfo to retrieve the error text.
            ERROR_INTERNET_FAILED_DUETOSECURITYCHECK = 12171,    //The function failed due to a security check.
            ERROR_INTERNET_FORCE_RETRY = 12032,    //The function needs to redo the request.
            ERROR_INTERNET_FORTEZZA_LOGIN_NEEDED = 12054,    //The requested resource requires Fortezza authentication.
            ERROR_INTERNET_HANDLE_EXISTS = 12036,    //The request failed because the handle already exists.
            ERROR_INTERNET_HTTP_TO_HTTPS_ON_REDIR = 12039,    //The application is moving from a non-SSL to an SSL connection because of a redirect.
            ERROR_INTERNET_HTTPS_HTTP_SUBMIT_REDIR = 12052,    //The data being submitted to an SSL connection is being redirected to a non-SSL connection.
            ERROR_INTERNET_HTTPS_TO_HTTP_ON_REDIR = 12040,    //The application is moving from an SSL to an non-SSL connection because of a redirect.
            ERROR_INTERNET_INCORRECT_FORMAT = 12027,    //The format of the request is invalid.
            ERROR_INTERNET_INCORRECT_HANDLE_STATE = 12019,    //The requested operation cannot be carried out because the handle supplied is not in the correct state.
            ERROR_INTERNET_INCORRECT_HANDLE_TYPE = 12018,    //The type of handle supplied is incorrect for this operation.
            ERROR_INTERNET_INCORRECT_PASSWORD = 12014,    //The request to connect and log on to an FTP server could not be completed because the supplied password is incorrect.
            ERROR_INTERNET_INCORRECT_USER_NAME = 12013,    //The request to connect and log on to an FTP server could not be completed because the supplied user name is incorrect.
            ERROR_INTERNET_INSERT_CDROM = 12053,    //The request requires a CD-ROM to be inserted in the CD-ROM drive to locate the resource requested.
            ERROR_INTERNET_INTERNAL_ERROR = 12004,    //An internal error has occurred.
            ERROR_INTERNET_INVALID_CA = 12045,    //The function is unfamiliar with the Certificate Authority that generated the server's certificate.
            ERROR_INTERNET_INVALID_OPERATION = 12016,    //The requested operation is invalid.
            ERROR_INTERNET_INVALID_OPTION = 12009,    //A request to InternetQueryOption or InternetSetOption specified an invalid option value.
            ERROR_INTERNET_INVALID_PROXY_REQUEST = 12033,    //The request to the proxy was invalid.
            ERROR_INTERNET_INVALID_URL = 12005,    //The URL is invalid.
            ERROR_INTERNET_ITEM_NOT_FOUND = 12028,    //The requested item could not be located.
            ERROR_INTERNET_LOGIN_FAILURE = 12015,    //The request to connect and log on to an FTP server failed.
            ERROR_INTERNET_LOGIN_FAILURE_DISPLAY_ENTITY_BODY = 12174,    //The MS-Logoff digest header has been returned from the Web site. This header specifically instructs the digest package to purge credentials for the associated realm. This error will only be returned if INTERNET_ERROR_MASK_LOGIN_FAILURE_DISPLAY_ENTITY_BODY has been set.
            ERROR_INTERNET_MIXED_SECURITY = 12041,    //The content is not entirely secure. Some of the content being viewed may have come from unsecured servers.
            ERROR_INTERNET_NAME_NOT_RESOLVED = 12007,    //The server name could not be resolved.
            ERROR_INTERNET_NEED_MSN_SSPI_PKG = 12173,    //Not currently implemented.
            ERROR_INTERNET_NEED_UI = 12034,    //A user interface or other blocking operation has been requested.
            ERROR_INTERNET_NO_CALLBACK = 12025,    //An asynchronous request could not be made because a callback function has not been set.
            ERROR_INTERNET_NO_CONTEXT = 12024,    //An asynchronous request could not be made because a zero context value was supplied.
            ERROR_INTERNET_NO_DIRECT_ACCESS = 12023,    //Direct network access cannot be made at this time.
            ERROR_INTERNET_NOT_INITIALIZED = 12172,    //Initialization of the WinINet API has not occurred. Indicates that a higher-level function, such as InternetOpen, has not been called yet.
            ERROR_INTERNET_NOT_PROXY_REQUEST = 12020,    //The request cannot be made via a proxy.
            ERROR_INTERNET_OPERATION_CANCELLED = 12017,    //The operation was canceled, usually because the handle on which the request was operating was closed before the operation completed.
            ERROR_INTERNET_OPTION_NOT_SETTABLE = 12011,    //The requested option cannot be set, only queried.
            ERROR_INTERNET_OUT_OF_HANDLES = 12001,    //No more handles could be generated at this time.
            ERROR_INTERNET_POST_IS_NON_SECURE = 12043,    //The application is posting data to a server that is not secure.
            ERROR_INTERNET_PROTOCOL_NOT_FOUND = 12008,    //The requested protocol could not be located.
            ERROR_INTERNET_PROXY_SERVER_UNREACHABLE = 12165,    //The designated proxy server cannot be reached.
            ERROR_INTERNET_REDIRECT_SCHEME_CHANGE = 12048,    //The function could not handle the redirection, because the scheme changed (for example, HTTP to FTP).
            ERROR_INTERNET_REGISTRY_VALUE_NOT_FOUND = 12021,    //A required registry value could not be located.
            ERROR_INTERNET_REQUEST_PENDING = 12026,    //The required operation could not be completed because one or more requests are pending.
            ERROR_INTERNET_RETRY_DIALOG = 12050,    //The dialog box should be retried.
            ERROR_INTERNET_SEC_CERT_CN_INVALID = 12038,    //SSL certificate common name (host name field) is incorrect梖or example, if you entered www.server.com and the common name on the certificate says www.different.com.
            ERROR_INTERNET_SEC_CERT_DATE_INVALID = 12037,    //SSL certificate date that was received from the server is bad. The certificate is expired.
            ERROR_INTERNET_SEC_CERT_ERRORS = 12055,    //The SSL certificate contains errors.
            ERROR_INTERNET_SEC_CERT_NO_REV = 12056,
            ERROR_INTERNET_SEC_CERT_REV_FAILED = 12057,
            ERROR_INTERNET_SEC_CERT_REVOKED = 12170,    //SSL certificate was revoked.
            ERROR_INTERNET_SEC_INVALID_CERT = 12169,    //SSL certificate is invalid.
            ERROR_INTERNET_SECURITY_CHANNEL_ERROR = 12157,    //The application experienced an internal error loading the SSL libraries.
            ERROR_INTERNET_SERVER_UNREACHABLE = 12164,    //The Web site or server indicated is unreachable.
            ERROR_INTERNET_SHUTDOWN = 12012,    //WinINet support is being shut down or unloaded.
            ERROR_INTERNET_TCPIP_NOT_INSTALLED = 12159,    //The required protocol stack is not loaded and the application cannot start WinSock.
            ERROR_INTERNET_TIMEOUT = 12002,    //The request has timed out.
            ERROR_INTERNET_UNABLE_TO_CACHE_FILE = 12158,    //The function was unable to cache the file.
            ERROR_INTERNET_UNABLE_TO_DOWNLOAD_SCRIPT = 12167,    //The automatic proxy configuration script could not be downloaded. The INTERNET_FLAG_MUST_CACHE_REQUEST flag was set.

            INET_E_INVALID_URL = unchecked((int)0x800C0002),
            INET_E_NO_SESSION = unchecked((int)0x800C0003),
            INET_E_CANNOT_CONNECT = unchecked((int)0x800C0004),
            INET_E_RESOURCE_NOT_FOUND = unchecked((int)0x800C0005),
            INET_E_OBJECT_NOT_FOUND = unchecked((int)0x800C0006),
            INET_E_DATA_NOT_AVAILABLE = unchecked((int)0x800C0007),
            INET_E_DOWNLOAD_FAILURE = unchecked((int)0x800C0008),
            INET_E_AUTHENTICATION_REQUIRED = unchecked((int)0x800C0009),
            INET_E_NO_VALID_MEDIA = unchecked((int)0x800C000A),
            INET_E_CONNECTION_TIMEOUT = unchecked((int)0x800C000B),
            INET_E_DEFAULT_ACTION = unchecked((int)0x800C0011),
            INET_E_INVALID_REQUEST = unchecked((int)0x800C000C),
            INET_E_UNKNOWN_PROTOCOL = unchecked((int)0x800C000D),
            INET_E_QUERYOPTION_UNKNOWN = unchecked((int)0x800C0013),
            INET_E_SECURITY_PROBLEM = unchecked((int)0x800C000E),
            INET_E_CANNOT_LOAD_DATA = unchecked((int)0x800C000F),
            INET_E_CANNOT_INSTANTIATE_OBJECT = unchecked((int)0x800C0010),
            INET_E_REDIRECT_FAILED = unchecked((int)0x800C0014),
            INET_E_REDIRECT_TO_DIR = unchecked((int)0x800C0015),
            INET_E_CANNOT_LOCK_REQUEST = unchecked((int)0x800C0016),
            INET_E_USE_EXTEND_BINDING = unchecked((int)0x800C0017),
            INET_E_TERMINATED_BIND = unchecked((int)0x800C0018),
            INET_E_ERROR_FIRST = unchecked((int)0x800C0002),
            INET_E_CODE_DOWNLOAD_DECLINED = unchecked((int)0x800C0100),
            INET_E_RESULT_DISPATCHED = unchecked((int)0x800C0200),
            INET_E_CANNOT_REPLACE_SFP_FILE = unchecked((int)0x800C0300),

            HTTP_COOKIE_DECLINED = 12162,    //The HTTP cookie was declined by the server.
            HTTP_COOKIE_NEEDS_CONFIRMATION = 12161,    //The HTTP cookie requires confirmation.
            HTTP_DOWNLEVEL_SERVER = 12151,    //The server did not return any headers.
            HTTP_HEADER_ALREADY_EXISTS = 12155,    //The header could not be added because it already exists.
            HTTP_HEADER_NOT_FOUND = 12150,    //The requested header could not be located.
            HTTP_INVALID_HEADER = 12153,    //The supplied header is invalid.
            HTTP_INVALID_QUERY_REQUEST = 12154,    //The request made to HttpQueryInfo is invalid.
            HTTP_INVALID_SERVER_RESPONSE = 12152,    //The server response could not be parsed.
            HTTP_NOT_REDIRECTED = 12160,    //The HTTP request was not redirected.
            HTTP_REDIRECT_FAILED = 12156,    //The redirection failed because either the scheme changed (for example, HTTP to FTP) or all attempts made to redirect failed (default is five attempts).
            HTTP_REDIRECT_NEEDS_CONFIRMATION = 12168    //The redirection requires user confirmation.
        }

        public enum WTS_INFO_CLASS
        {
            WTSInitialProgram,
            WTSApplicationName,
            WTSWorkingDirectory,
            WTSOEMId,
            WTSSessionId,
            WTSUserName,
            WTSWinStationName,
            WTSDomainName,
            WTSConnectState,
            WTSClientBuildNumber,
            WTSClientName,
            WTSClientDirectory,
            WTSClientProductId,
            WTSClientHardwareId,
            WTSClientAddress,
            WTSClientDisplay,
            WTSClientProtocolType
        }

        public enum CreationFlags
        {
            CREATE_SUSPENDED = 0x00000004,
            CREATE_NEW_CONSOLE = 0x00000010,
            CREATE_NEW_PROCESS_GROUP = 0x00000200,
            CREATE_UNICODE_ENVIRONMENT = 0x00000400,
            CREATE_SEPARATE_WOW_VDM = 0x00000800,
            CREATE_DEFAULT_ERROR_MODE = 0x04000000,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct IELAUNCHURLINFO
        {
            public int cbSize;
            public int dwCreationFlags;
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct GUID
        {
            public int a;
            public short b;
            public short c;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] d;
        }

        [ComVisible(true), StructLayout(LayoutKind.Sequential)]
        public struct tagPOINT
        {
            [MarshalAs(UnmanagedType.I4)]
            public int X;
            [MarshalAs(UnmanagedType.I4)]
            public int Y;
        }

        [ComVisible(true), StructLayout(LayoutKind.Sequential)]
        public struct tagSIZE
        {
            [MarshalAs(UnmanagedType.I4)]
            public int cx;
            [MarshalAs(UnmanagedType.I4)]
            public int cy;
        }

        [ComVisible(true), StructLayout(LayoutKind.Sequential)]
        public struct tagSIZEL
        {
            [MarshalAs(UnmanagedType.I4)]
            public int cx;
            [MarshalAs(UnmanagedType.I4)]
            public int cy;
        }

        [ComVisible(true), StructLayout(LayoutKind.Sequential)]
        public struct tagRECT
        {
            [MarshalAs(UnmanagedType.I4)]
            public int Left;
            [MarshalAs(UnmanagedType.I4)]
            public int Top;
            [MarshalAs(UnmanagedType.I4)]
            public int Right;
            [MarshalAs(UnmanagedType.I4)]
            public int Bottom;

            public tagRECT(int left_, int top_, int right_, int bottom_)
            {
                Left = left_;
                Top = top_;
                Right = right_;
                Bottom = bottom_;
            }
        }

        [ComVisible(true), StructLayout(LayoutKind.Sequential)]
        public struct tagMSG
        {
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.I4)]
            public int message;
            public IntPtr wParam;
            public IntPtr lParam;
            [MarshalAs(UnmanagedType.I4)]
            public int time;
            // pt was a by-value POINT structure
            [MarshalAs(UnmanagedType.I4)]
            public int pt_x;
            [MarshalAs(UnmanagedType.I4)]
            public int pt_y;
            //public tagPOINT pt;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct PROFILEINFO
        {
            public int dwSize;
            public int dwFlags;

            [MarshalAs(UnmanagedType.LPTStr)]
            public String lpUserName;

            [MarshalAs(UnmanagedType.LPTStr)]
            public String lpProfilePath;

            [MarshalAs(UnmanagedType.LPTStr)]
            public String lpDefaultPath;

            [MarshalAs(UnmanagedType.LPTStr)]
            public String lpServerName;

            [MarshalAs(UnmanagedType.LPTStr)]
            public String lpPolicyPath;
            public IntPtr hProfile;
        }

        #endregion

        #region advapi.dll

        // Define custom commands for the SimpleService.
        public enum SimpleServiceCustomCommands
        {
            StopWorker = 128,
            RestartWorker,
            CheckWorker
        };
        [StructLayout(LayoutKind.Sequential)]
        public struct SERVICE_STATUS
        {
            public int serviceType;
            public int currentState;
            public int controlsAccepted;
            public int win32ExitCode;
            public int serviceSpecificExitCode;
            public int checkPoint;
            public int waitHint;
        }

        public enum State
        {
            SERVICE_STOPPED = 0x00000001,
            SERVICE_START_PENDING = 0x00000002,
            SERVICE_STOP_PENDING = 0x00000003,
            SERVICE_RUNNING = 0x00000004,
            SERVICE_CONTINUE_PENDING = 0x00000005,
            SERVICE_PAUSE_PENDING = 0x00000006,
            SERVICE_PAUSED = 0x00000007,
        }

        [Flags]
        public enum ACCESS_MASK : uint
        {
            DELETE = 0x00010000,
            READ_CONTROL = 0x00020000,
            WRITE_DAC = 0x00040000,
            WRITE_OWNER = 0x00080000,
            SYNCHRONIZE = 0x00100000,

            STANDARD_RIGHTS_REQUIRED = 0x000f0000,

            STANDARD_RIGHTS_READ = 0x00020000,
            STANDARD_RIGHTS_WRITE = 0x00020000,
            STANDARD_RIGHTS_EXECUTE = 0x00020000,

            STANDARD_RIGHTS_ALL = 0x001f0000,

            SPECIFIC_RIGHTS_ALL = 0x0000ffff,

            ACCESS_SYSTEM_SECURITY = 0x01000000,

            MAXIMUM_ALLOWED = 0x02000000,

            GENERIC_READ = 0x80000000,
            GENERIC_WRITE = 0x40000000,
            GENERIC_EXECUTE = 0x20000000,
            GENERIC_ALL = 0x10000000,

            DESKTOP_READOBJECTS = 0x00000001,
            DESKTOP_CREATEWINDOW = 0x00000002,
            DESKTOP_CREATEMENU = 0x00000004,
            DESKTOP_HOOKCONTROL = 0x00000008,
            DESKTOP_JOURNALRECORD = 0x0000010,
            DESKTOP_JOURNALPLAYBACK = 0x00000020,
            DESKTOP_ENUMERATE = 0x00000040,
            DESKTOP_WRITEOBJECTS = 0x00000080,
            DESKTOP_SWITCHDESKTOP = 0x00000100,

            WINSTA_ENUMDESKTOPS = 0x00000001,
            WINSTA_READATTRIBUTES = 0x00000002,
            WINSTA_ACCESSCLIPBOARD = 0x00000004,
            WINSTA_CREATEDESKTOP = 0x00000008,
            WINSTA_WRITEATTRIBUTES = 0x00000010,
            WINSTA_ACCESSGLOBALATOMS = 0x00000020,
            WINSTA_EXITWINDOWS = 0x00000040,
            WINSTA_ENUMERATE = 0x00000100,
            WINSTA_READSCREEN = 0x00000200,

            WINSTA_ALL_ACCESS = 0x0000037f
        };

        // Desired access flags
        public static uint STANDARD_RIGHTS_REQUIRED = 0x000F0000;
        public static uint STANDARD_RIGHTS_READ = 0x00020000;
        public static uint TOKEN_ASSIGN_PRIMARY = 0x0001;
        public static uint TOKEN_DUPLICATE = 0x0002;
        public static uint TOKEN_IMPERSONATE = 0x0004;
        public static uint TOKEN_QUERY = 0x0008;
        public static uint TOKEN_QUERY_SOURCE = 0x0010;
        public static uint TOKEN_ADJUST_PRIVILEGES = 0x0020;
        public static uint TOKEN_ADJUST_GROUPS = 0x0040;
        public static uint TOKEN_ADJUST_DEFAULT = 0x0080;
        public static uint TOKEN_ADJUST_SESSIONID = 0x0100;
        public static uint TOKEN_READ = (STANDARD_RIGHTS_READ | TOKEN_QUERY);
        public static uint TOKEN_ALL_ACCESS = (STANDARD_RIGHTS_REQUIRED | TOKEN_ASSIGN_PRIMARY |
                                               TOKEN_DUPLICATE | TOKEN_IMPERSONATE | TOKEN_QUERY | TOKEN_QUERY_SOURCE |
                                               TOKEN_ADJUST_PRIVILEGES | TOKEN_ADJUST_GROUPS | TOKEN_ADJUST_DEFAULT |
                                               TOKEN_ADJUST_SESSIONID);

        //
        // Group attributes
        //
        public static uint SE_GROUP_MANDATORY = 0x00000001;
        public static uint SE_GROUP_ENABLED_BY_DEFAULT = 0x00000002;
        public static uint SE_GROUP_ENABLED = 0x00000004;
        public static uint SE_GROUP_OWNER = 0x00000008;
        public static uint SE_GROUP_USE_FOR_DENY_ONLY = 0x00000010;
        public static uint SE_GROUP_INTEGRITY = 0x00000020;
        public static uint SE_GROUP_INTEGRITY_ENABLED = 0x00000040;
        public static uint SE_GROUP_LOGON_ID = 0xC0000000;
        public static uint SE_GROUP_RESOURCE = 0x20000000;
        public static uint SE_GROUP_VALID_ATTRIBUTES = (SE_GROUP_MANDATORY |
                                                        SE_GROUP_ENABLED_BY_DEFAULT |
                                                        SE_GROUP_ENABLED |
                                                        SE_GROUP_OWNER |
                                                        SE_GROUP_USE_FOR_DENY_ONLY |
                                                        SE_GROUP_LOGON_ID |
                                                        SE_GROUP_RESOURCE |
                                                        SE_GROUP_INTEGRITY |
                                                        SE_GROUP_INTEGRITY_ENABLED);

        //
        // Privilege Constants 
        // Privileges determine the type of system operations that a user account can perform. 
        // An administrator assigns privileges to user and group accounts. 
        // Each user's privileges include those granted to the user and to the groups to which the user belongs.

        public static string SE_CREATE_TOKEN_NAME = "SeCreateTokenPrivilege";
        public static string SE_ASSIGNPRIMARYTOKEN_NAME = "SeAssignPrimaryTokenPrivilege";
        public static string SE_LOCK_MEMORY_NAME = "SeLockMemoryPrivilege";
        public static string SE_INCREASE_QUOTA_NAME = "SeIncreaseQuotaPrivilege";
        public static string SE_UNSOLICITED_INPUT_NAME = "SeUnsolicitedInputPrivilege";
        public static string SE_MACHINE_ACCOUNT_NAME = "SeMachineAccountPrivilege";
        public static string SE_TCB_NAME = "SeTcbPrivilege";
        public static string SE_SECURITY_NAME = "SeSecurityPrivilege";
        public static string SE_TAKE_OWNERSHIP_NAME = "SeTakeOwnershipPrivilege";
        public static string SE_LOAD_DRIVER_NAME = "SeLoadDriverPrivilege";
        public static string SE_SYSTEM_PROFILE_NAME = "SeSystemProfilePrivilege";
        public static string SE_SYSTEMTIME_NAME = "SeSystemtimePrivilege";
        public static string SE_PROF_SINGLE_PROCESS_NAME = "SeProfileSingleProcessPrivilege";
        public static string SE_INC_BASE_PRIORITY_NAME = "SeIncreaseBasePriorityPrivilege";
        public static string SE_CREATE_PAGEFILE_NAME = "SeCreatePagefilePrivilege";
        public static string SE_CREATE_PERMANENT_NAME = "SeCreatePermanentPrivilege";
        public static string SE_BACKUP_NAME = "SeBackupPrivilege";
        public static string SE_RESTORE_NAME = "SeRestorePrivilege";
        public static string SE_SHUTDOWN_NAME = "SeShutdownPrivilege";
        public static string SE_DEBUG_NAME = "SeDebugPrivilege";
        public static string SE_AUDIT_NAME = "SeAuditPrivilege";
        public static string SE_SYSTEM_ENVIRONMENT_NAME = "SeSystemEnvironmentPrivilege";
        public static string SE_CHANGE_NOTIFY_NAME = "SeChangeNotifyPrivilege";
        public static string SE_REMOTE_SHUTDOWN_NAME = "SeRemoteShutdownPrivilege";
        public static string SE_CREATE_GLOBAL_NAME = "SeCreateGlobalPrivilege";
        public static string SE_UNDOCK_NAME = "SeUndockPrivilege";
        public static string SE_MANAGE_VOLUME_NAME = "SeManageVolumePrivilege";
        public static string SE_IMPERSONATE_NAME = "SeImpersonatePrivilege";
        public static string SE_ENABLE_DELEGATION_NAME = "SeEnableDelegationPrivilege";
        public static string SE_SYNC_AGENT_NAME = "SeSyncAgentPrivilege";
        public static string SE_TRUSTED_CREDMAN_ACCESS_NAME = "SeTrustedCredManAccessPrivilege";
        public static string SE_RELABEL_NAME = "SeRelabelPrivilege";
        public static string SE_INCREASE_WORKING_SET_NAME = "SeIncreaseWorkingSetPrivilege";
        public static string SE_TIME_ZONE_NAME = "SeTimeZonePrivilege";
        public static string SE_CREATE_SYMBOLIC_LINK_NAME = "SeCreateSymbolicLinkPrivilege";

        public static UInt32 SE_PRIVILEGE_ENABLED_BY_DEFAULT = 0x00000001;
        public static UInt32 SE_PRIVILEGE_ENABLED = 0x00000002;
        public static UInt32 SE_PRIVILEGE_REMOVED = 0x00000004;
        public static UInt32 SE_PRIVILEGE_USED_FOR_ACCESS = 0x80000000;

        //
        // Privileges are represented by LUIDs and have attributes indicating whether they are currently enabled or disabled.
        //

        public const int ANYSIZE_ARRAY = 1;

        [StructLayout(LayoutKind.Sequential)]
        public struct LUID
        {
            public int LowPart;
            public int HighPart;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct LUID_AND_ATTRIBUTES
        {
            public LUID Luid;
            public UInt32 Attributes;
        }

        public struct TOKEN_PRIVILEGES
        {
            public int PrivilegeCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = ANYSIZE_ARRAY)]
            public LUID_AND_ATTRIBUTES[] Privileges;
        }
        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool LookupPrivilegeValue(string lpSystemName, string lpName, out LUID lpLuid);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool AdjustTokenPrivileges(
            IntPtr TokenHandle,
            [MarshalAs(UnmanagedType.Bool)]bool DisableAllPrivileges,
            ref TOKEN_PRIVILEGES NewState,
            UInt32 BufferLength,
            IntPtr PreviousState, //TOKEN_PRIVILEGES PreviousState,
            IntPtr ReturnLength
            );

        public enum STARTF : uint
        {
            STARTF_USESHOWWINDOW = 0x00000001,
            STARTF_USESIZE = 0x00000002,
            STARTF_USEPOSITION = 0x00000004,
            STARTF_USECOUNTCHARS = 0x00000008,
            STARTF_USEFILLATTRIBUTE = 0x00000010,
            STARTF_RUNFULLSCREEN = 0x00000020,  // ignored for non-x86 platforms
            STARTF_FORCEONFEEDBACK = 0x00000040,
            STARTF_FORCEOFFFEEDBACK = 0x00000080,
            STARTF_USESTDHANDLES = 0x00000100,
        };

        public enum SHOWWINDOW : uint
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11,
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SECURITY_ATTRIBUTES
        {
            public int nLength;
            public IntPtr lpSecurityDescriptor;
            public int bInheritHandle;
        };

        public enum SECURITY_IMPERSONATION_LEVEL
        {
            SecurityAnonymous,
            SecurityIdentification,
            SecurityImpersonation,
            SecurityDelegation
        };

        public enum TOKEN_INFORMATION_CLASS
        {
            TokenUser = 1,
            TokenGroups,
            TokenPrivileges,
            TokenOwner,
            TokenPrimaryGroup,
            TokenDefaultDacl,
            TokenSource,
            TokenType,
            TokenImpersonationLevel,
            TokenStatistics,
            TokenRestrictedSids,
            TokenSessionId,
            TokenGroupsAndPrivileges,
            TokenSessionReference,
            TokenSandBoxInert,
            TokenAuditPolicy,
            TokenOrigin,
            TokenElevationType,
            TokenLinkedToken,
            TokenElevation,
            TokenHasRestrictions,
            TokenAccessInformation,
            TokenVirtualizationAllowed,
            TokenVirtualizationEnabled,
            TokenIntegrityLevel,
            TokenUIAccess,
            TokenMandatoryPolicy,
            TokenLogonSid,
            MaxTokenInfoClass  // MaxTokenInfoClass should always be the last enum
        };

        public struct TOKEN_USER
        {
            public SID_AND_ATTRIBUTES User;
        }

        public enum TOKEN_TYPE
        {
            TokenPrimary = 1,
            TokenImpersonation
        };

        [StructLayout(LayoutKind.Sequential)]
        public struct SID_AND_ATTRIBUTES
        {

            public IntPtr Sid;
            public uint Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct TOKEN_MANDATORY_LABEL
        {
            public SID_AND_ATTRIBUTES Label;
        }

        [Flags]
        public enum SECURITY_MANDATORY : uint
        {
            SECURITY_MANDATORY_UNTRUSTED_RID = 0x00000000,
            SECURITY_MANDATORY_LOW_RID = 0x00001000,
            SECURITY_MANDATORY_MEDIUM_RID = 0x00002000,
            SECURITY_MANDATORY_HIGH_RID = 0x00003000,
            SECURITY_MANDATORY_SYSTEM_RID = 0x00004000,
            SECURITY_MANDATORY_PROTECTED_PROCESS_RID = 0x00005000
        }

        [DllImport("ADVAPI32.DLL", EntryPoint = "SetServiceStatus")]
        public static extern bool SetServiceStatus(IntPtr hServiceStatus, SERVICE_STATUS lpServiceStatus);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool ConvertStringSidToSid(string StringSid, out IntPtr ptrSid);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CreateProcess(String lpApplicationName, String lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, bool bInheritHandles, uint dwCreationFlags,
                                                                            IntPtr lpEnvironment, string lpCurrentDirectory, ref STARTUPINFO lpStartUpInfo, out PROCESS_INFORMATION lpProcessInformation);

        [DllImport("advapi32.dll", EntryPoint = "CreateProcessAsUser", SetLastError = true, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.StdCall)]
        public extern static bool CreateProcessAsUser(IntPtr hToken, String lpApplicationName, String lpCommandLine, IntPtr lpProcessAttributes, IntPtr lpThreadAttributes, bool bInheritHandle,
                                                       int dwCreationFlags, IntPtr lpEnvironment, String lpCurrentDirectory, ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

        [DllImport("advapi32.dll", EntryPoint = "CreateProcessAsUser", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool CreateProcessAsUser(IntPtr in_ptrUserTokenHandle,
                                                      String in_strApplicationName,
                                                      String in_strCommandLine,
                                                      ref Win32.SECURITY_ATTRIBUTES in_oProcessAttributes,
                                                      ref Win32.SECURITY_ATTRIBUTES in_oThreadAttributes,
                                                      bool in_bInheritHandles,
                                                      CreationFlags in_eCreationFlags,
                                                      IntPtr in_ptrEnvironmentBlock,
                                                      String in_strCurrentDirectory,
                                                      ref Win32.STARTUPINFO in_oStartupInfo,
                                                      ref Win32.PROCESS_INFORMATION in_oProcessInformation);

        [DllImport("advapi32.dll", CallingConvention = CallingConvention.Winapi, SetLastError = true, CharSet = CharSet.Auto)]
        public extern static bool CreateProcessWithTokenW(IntPtr hToken,
                                                          uint dwLogonFlags,
                                                          String lpApplicationName,
                                                          String lpCommandLine,
                                                          uint dwCreationFlags,
                                                          IntPtr pEnvironment,
                                                          String lpCurrentDirectory,
                                                          ref STARTUPINFO lpStartupInfo,
                                                          out PROCESS_INFORMATION lpProcessInfo);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true, EntryPoint = "ConvertStringSecurityDescriptorToSecurityDescriptor", ExactSpelling = false)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ConvertStringSecurityDescriptorToSecurityDescriptor([MarshalAs(UnmanagedType.LPTStr)] string StringSecurityDescriptor,
                                                                                        int StringSDRevision,
                                                                                        out IntPtr SecurityDescriptor,
                                                                                        out int SecurityDescriptorSize
                                                                                     );

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public extern static bool DuplicateTokenEx(IntPtr hExistingToken,
                                                    uint dwDesiredAccess,
                                                    ref IntPtr/*SECURITY_ATTRIBUTES*/ lpTokenAttributes,
                                                    SECURITY_IMPERSONATION_LEVEL ImpersonationLevel,
                                                    TOKEN_TYPE TokenType,
                                                    out IntPtr phNewToken);

        [DllImport("advapi32.dll")]
        public static extern uint GetLengthSid(IntPtr pSid);

        [DllImport("advapi32.dll", CallingConvention = CallingConvention.Winapi, SetLastError = true, CharSet = CharSet.Auto)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetSecurityDescriptorSacl(IntPtr pSecurityDescriptor,
                                                             out bool lpbSaclPresent,
                                                             ref IntPtr pSacl,     // By ref, because if "present" == false, value is unchanged
                                                             out bool lpbSaclDefaulted
            );

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern IntPtr GetSidSubAuthorityCount(IntPtr pSid);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern IntPtr GetSidSubAuthority(IntPtr pSid, uint nSubAuthority);

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool GetTokenInformation(IntPtr TokenHandle, TOKEN_INFORMATION_CLASS TokenInformationClass, IntPtr TokenInformation, uint TokenInformationLength, out uint ReturnLength);

        // Using IntPtr for pSID insted of Byte[]
        [DllImport("advapi32", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool ConvertSidToStringSid(IntPtr pSID, out IntPtr ptrSid);

        [DllImport("advapi32.dll", SetLastError = true)]
        static extern public bool OpenProcessToken(IntPtr ProcessHandle, UInt32 DesiredAccess, out IntPtr TokenHandle);

        [DllImport("advapi32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static public extern bool SetTokenInformation(IntPtr TokenHandle, TOKEN_INFORMATION_CLASS TokenInformationClass, ref TOKEN_MANDATORY_LABEL TokenInformation, uint TokenInformationLength);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = false, ExactSpelling = true)]
        public static extern Int32 SetSecurityInfo(IntPtr objectHandle, int seObjectType, int fSecurityInfo, IntPtr psidOwner, IntPtr pidGroup, IntPtr pDacl, IntPtr pSacl);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueEx")]
        public static extern int RegQueryValueEx(IntPtr keyBase, string valueName, IntPtr reserved, ref RegistryValueKind type, IntPtr zero, ref int dataSize);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueEx")]
        public static extern int RegQueryValueEx(IntPtr keyBase, string valueName, IntPtr reserved, ref RegistryValueKind type, [Out] byte[] data, ref int dataSize);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueEx")]
        public static extern int RegQueryValueEx(IntPtr keyBase, string valueName, IntPtr reserved, ref RegistryValueKind type, ref int data, ref int dataSize);

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, EntryPoint = "RegOpenKeyExW", SetLastError = true)]
        public static extern int RegOpenKeyExW(UIntPtr hKey, string subKey, uint options, int sam, out IntPtr phkResult);

        [DllImport("advapi32.dll", EntryPoint = "RegQueryInfoKeyW", CallingConvention = CallingConvention.Winapi)]
        [MethodImpl(MethodImplOptions.PreserveSig)]
        extern public static int RegQueryInfoKey(IntPtr hkey, byte[] lpClass, IntPtr lpcbClass, IntPtr lpReserved, out uint lpcSubKeys, IntPtr lpcbMaxSubKeyLen, IntPtr lpcbMaxClassLen, out uint lpcValues, IntPtr lpcbMaxValueNameLen, IntPtr lpcbMaxValueLen, IntPtr lpcbSecurityDescriptor, IntPtr lpftLastWriteTime);

        [DllImport("advapi32.dll", EntryPoint = "RegEnumKeyExW", CallingConvention = CallingConvention.Winapi)]
        [MethodImpl(MethodImplOptions.PreserveSig)]
        extern public static int RegEnumKeyEx(IntPtr hkey, uint index, byte[] lpName, ref uint lpcbName, IntPtr reserved, IntPtr lpClass, IntPtr lpcbClass, out long lpftLastWriteTime);

        #endregion

        #region core.dll

        #region Error Codes
        public const int ERROR_SUCCESS = 0;
        public const int ERROR_INSUFFICIENT_BUFFER = 122;
        #endregion

        [DllImport("coredll.dll", SetLastError = true)]
        public static extern Int32 GetLastError();

        #endregion

        #region ieframe.dll

        /// <summary>
        /// WARNING, this method can only be used on Vista!!!!!
        /// </summary>
        /// <param name="url"></param>
        /// <param name="procInfo"></param>
        /// <param name="info"></param>
        [DllImport("ieframe.dll", CharSet = CharSet.Unicode)]
        public static extern void IELaunchURL(
            string url,
            out PROCESS_INFORMATION procInfo,
            out IntPtr info		// use IntPtr.Zero
            );

        /// <summary>
        /// WARNING, this method can only be used on Vista!!!!!
        /// Determines if a URL will open in a protected mode process
        /// </summary>
        /// <param name="url"></param>
        [DllImport("ieframe.dll", CharSet = CharSet.Unicode)]
        public static extern int IEIsProtectedModeURL(string url);


        #endregion

        #region kernel32.dll

        public const UInt32 INFINITE = 0xFFFFFFFF;
        public const UInt32 WAIT_ABANDONED = 0x00000080;
        public const UInt32 WAIT_OBJECT_0 = 0x00000000;
        public const UInt32 WAIT_TIMEOUT = 0x00000102;
        public const uint STILL_ACTIVE = 259;

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct STARTUPINFO
        {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public UInt32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [Flags()]
        public enum ProcessAccessFlags : int
        {
            /// <summary>Specifies all possible access flags for the process object.</summary>
            AllAccess = CreateThread | DuplicateHandle | QueryInformation | SetInformation
                | Terminate | VMOperation | VMRead | VMWrite | Synchronize,
            /// <summary>Enables usage of the process handle in the CreateRemoteThread
            /// function to create a thread in the process.</summary>
            CreateThread = 0x2,
            /// <summary>Enables usage of the process handle as either the source or target process
            /// in the DuplicateHandle function to duplicate a handle.</summary>
            DuplicateHandle = 0x40,
            /// <summary>Enables usage of the process handle in the GetExitCodeProcess and
            /// GetPriorityClass functions to read information from the process object.</summary>
            QueryInformation = 0x400,
            /// <summary>Enables usage of the process handle in the SetPriorityClass function to
            /// set the priority class of the process.</summary>
            SetInformation = 0x200,
            /// <summary>Enables usage of the process handle in the TerminateProcess function to
            /// terminate the process.</summary>
            Terminate = 0x1,
            /// <summary>Enables usage of the process handle in the VirtualProtectEx and
            /// WriteProcessMemory functions to modify the virtual memory of the process.</summary>
            VMOperation = 0x8,
            /// <summary>Enables usage of the process handle in the ReadProcessMemory function to'
            /// read from the virtual memory of the process.</summary>
            VMRead = 0x10,
            /// <summary>Enables usage of the process handle in the WriteProcessMemory function to
            /// write to the virtual memory of the process.</summary>
            VMWrite = 0x20,
            /// <summary>Enables usage of the process handle in any of the wait functions to wait
            /// for the process to terminate.</summary>
            Synchronize = 0x100000
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        internal struct OSVERSIONINFOEX
        {
            public uint dwOSVersionInfoSize;
            public uint dwMajorVersion;
            public uint dwMinorVersion;
            public uint dwBuildNumber;
            public uint dwPlatformId;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 128)]
            public string szCSDVersion;
            public UInt16 wServicePackMajor;
            public UInt16 wServicePackMinor;
            public UInt16 wSuiteMask;
            public byte wProductType;
            public byte wReserved;
        }

        [DllImport("kernel32", SetLastError = true)]
        internal static extern bool GetVersionEx(ref OSVERSIONINFOEX osvi);

        [DllImport("kernel32", SetLastError = true)]
        internal static extern bool GetProductInfo(UInt32 dwOSMajorVersion, UInt32 dwOSMinorVersion, UInt32 dwSpMajorVersion,
                                                   UInt32 dwSpMinorVersion, out UInt32 pdwReturnedProductType);
        [DllImport("user32.dll")]
        internal static extern int GetSystemMetrics(Int32 smIndex);

        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, [MarshalAs(UnmanagedType.Bool)] bool bInheritHandle, uint dwProcessId);

        [DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool wow64Process);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static public extern void SetDllDirectory(string lpPathName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        static public extern UInt32 LoadLibrary(string libraryName);

        [DllImport("kernel32.dll", SetLastError = true)]
        static public extern void FreeLibrary(IntPtr hLib);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        static public extern IntPtr GetProcAddress(UInt32 hLibrary, string methodName);

        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        static private extern IntPtr GetProcAddress(IntPtr hLibrary, string methodName);

        [DllImport("kernel32.dll", EntryPoint = "CloseHandle", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32", SetLastError = true)]
        static public extern UInt32 WaitForSingleObject(IntPtr handle, UInt32 milliseconds);

        [DllImport("kernel32.dll")]
        static public extern IntPtr GetCurrentProcess();

        [DllImport("kernel32.dll")]
        static public extern IntPtr GetCurrentThreadId();

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static public extern bool GetExitCodeProcess(IntPtr hProcess, out uint lpExitCode);

        [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
        static public extern uint GetPrivateProfileStringW(string lpAppName, string lpKeyName, string lpDefault, StringBuilder lpReturnedString, uint nSize, string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LocalFree(IntPtr hMem);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr CreateFile([MarshalAs(UnmanagedType.LPTStr)] string lpFileName, int dwDesiredAccess, int dwShareMode, IntPtr lpSecurityAttributes, int dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32.dll")]
        public static extern uint GetProcessId(IntPtr Process);

        [DllImport("kernel32.dll")]
        public static extern uint TerminateProcess(IntPtr hProcess, uint exitCode);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("kernel32.dll")]
        public static extern bool GlobalUnlock(IntPtr hMem);

        [DllImport("kernel32.dll")]
        public static extern UIntPtr GlobalSize(IntPtr hMem);

        [StructLayout(LayoutKind.Sequential)]
        public struct FILETIME
        {
            public uint DateTimeLow;
            public uint DateTimeHigh;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SYSTEMTIME
        {
            [MarshalAs(UnmanagedType.U2)]
            public short Year;
            [MarshalAs(UnmanagedType.U2)]
            public short Month;
            [MarshalAs(UnmanagedType.U2)]
            public short DayOfWeek;
            [MarshalAs(UnmanagedType.U2)]
            public short Day;
            [MarshalAs(UnmanagedType.U2)]
            public short Hour;
            [MarshalAs(UnmanagedType.U2)]
            public short Minute;
            [MarshalAs(UnmanagedType.U2)]
            public short Second;
            [MarshalAs(UnmanagedType.U2)]
            public short Milliseconds;
        }

        [DllImport("Kernel32.dll", SetLastError = true)]
        public static extern long FileTimeToSystemTime(ref FILETIME FileTime, ref SYSTEMTIME SystemTime);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern long SystemTimeToTzSpecificLocalTime(IntPtr lpTimeZoneInformation, ref SYSTEMTIME lpUniversalTime, out SYSTEMTIME lpLocalTime);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetProcessWorkingSetSize(IntPtr proc, int min, int max);

        [DllImport("kernel32.dll", EntryPoint = "WTSGetActiveConsoleSessionId", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern UInt32 WTSGetActiveConsoleSessionId();

        // imports system functions for work with pointers
        [DllImport("kernel32")]
        public extern static IntPtr HeapAlloc(IntPtr heap, UInt32 flags, UInt32 bytes);

        [DllImport("kernel32")]
        public extern static IntPtr GetProcessHeap();

        [DllImport("kernel32")]
        public extern static int lstrlen(IntPtr str);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool HeapFree(IntPtr hHeap, uint dwFlags, IntPtr lpMem);

        #endregion

        #region shell32.dll

        //  KNOWNFOLDERID based APIs
        public static uint KF_FLAG_CREATE = 0x00008000;                 // Make sure that the folder already exists or create it and apply security specified in folder definition                                                // If folder can not be created then function will return failure and no folder path (IDList) will be returned
        // If folder is located on the network the function may take long time to execute

        public static uint KF_FLAG_DONT_VERIFY = 0x00004000;            // If this flag is specified then the folder path is returned and no verification is performed
        // Use this flag is you want to get folder's path (IDList) and do not need to verify folder's existence
        //
        // If this flag is NOT specified then Known Folder API will try to verify that the folder exists
        //     If folder does not exist or can not be accessed then function will return failure and no folder path (IDList) will be returned
        //     If folder is located on the network the function may take long time to execute

        public static uint KF_FLAG_DONT_UNEXPAND = 0x00002000;          // Set folder path as is and do not try to substitute parts of the path with environments variables.
        // If flag is not specified then Known Folder will try to replace parts of the path with some
        // known environment variables (%USERPROFILE%, %APPDATA% etc.)

        public static uint KF_FLAG_NO_ALIAS = 0x00001000;               // Get file system based IDList if available. If the flag is not specified the Known Folder API
        // will try to return aliased IDList by default. Example for FOLDERID_Documents -
        // Aliased - [desktop]\[user]\[Documents] - exact location is determined by shell namespace layout and might change
        // Non aliased - [desktop]\[computer]\[disk_c]\[users]\[user]\[Documents] - location is determined by folder location in the file system

        public static uint KF_FLAG_INIT = 0x00000800;                   // Initialize the folder with desktop.ini settings
        // If folder can not be initialized then function will return failure and no folder path will be returned
        // If folder is located on the network the function may take long time to execute

        public static uint KF_FLAG_DEFAULT_PATH = 0x00000400;           // Get the default path, will also verify folder existence unless KF_FLAG_DONT_VERIFY is also specified

        public static uint KF_FLAG_NOT_PARENT_RELATIVE = 0x00000200;    // Get the not-parent-relative default path. Only valid with KF_FLAG_DEFAULT_PATH

        public static uint KF_FLAG_SIMPLE_IDLIST = 0x00000100;          // Build simple pidl

        // Known folders
        public static Guid FOLDERID_LocalAppDataLow = new Guid("A520A1A4-1780-4FF6-BD18-167343C5AF16");
        public static Guid FOLDERID_CommonStartup = new Guid("82A5EA35-D9CD-47C5-9629-E15D2F714E6E");

        [DllImport("shell32.dll")]
        public static extern int SHGetKnownFolderPath([In] ref Guid rfid, uint dwFlags, IntPtr hToken, ref IntPtr szPath);

        public const int ABM_GETTASKBARPOS = 5;

        [StructLayout(LayoutKind.Sequential)]
        public struct APPBARDATA
        {
            public int cbSize;
            public IntPtr hWnd;
            public uint uCallbackMessage;
            public uint uEdge;
            public Rect rc;
            public int lParam;
        }

        public enum TaskBarEdge
        {
            Left = 0,
            Top = 1,
            Right = 2,
            Bottom = 3
        }

        [DllImport("shell32.dll")]
        public static extern uint SHAppBarMessage(uint dwMessage, [In] ref APPBARDATA pData);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);

        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        public static extern uint DragQueryFile(IntPtr hDrop, uint iFile, [Out] StringBuilder lpszFile, uint cch);

        #endregion

        #region user32.dll

        //values from Winuser.h in Microsoft SDK.
        public const int WH_MOUSE_LL = 14;	//mouse hook constant
        public const int WH_KEYBOARD_LL = 13;	//keyboard hook constant	

        [StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct Rect
        {
            public Int32 left;
            public Int32 top;
            public Int32 right;
            public Int32 bottom;
        };

        public struct WINDOWINFO
        {
            public uint cbSize;
            public Rect rcWindow;
            public Rect rcClient;
            public uint dwStyle;
            public uint dwExStyle;
            public uint dwWindowStatus;
            public uint cxWindowBorders;
            public uint cyWindowBorders;
            public ushort atomWindowType;
            public ushort wCreatorVersion;
        };

        //Declare wrapper managed POINT class.
        [StructLayout(LayoutKind.Sequential)]
        public class POINT
        {
            public int x;
            public int y;
        }

        //Declare wrapper managed MouseHookStruct class.
        [StructLayout(LayoutKind.Sequential)]
        public class MouseHookStruct
        {
            public POINT pt;
            public int hwnd;
            public int wHitTestCode;
            public int dwExtraInfo;
        }


        /// <summary>
        /// The MSLLHOOKSTRUCT structure contains information about a low-level keyboard input event. 
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public class MouseLLHookStruct
        {
            /// <summary>
            /// Specifies a POINT structure that contains the x- and y-coordinates of the cursor, in screen coordinates. 
            /// </summary>
            public POINT pt;
            /// <summary>
            /// If the message is WM_MOUSEWHEEL, the high-order word of this member is the wheel delta. 
            /// The low-order word is reserved. A positive value indicates that the wheel was rotated forward, 
            /// away from the user; a negative value indicates that the wheel was rotated backward, toward the user. 
            /// One wheel click is defined as WHEEL_DELTA, which is 120. 
            ///If the message is WM_XBUTTONDOWN, WM_XBUTTONUP, WM_XBUTTONDBLCLK, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP,
            /// or WM_NCXBUTTONDBLCLK, the high-order word specifies which X button was pressed or released, 
            /// and the low-order word is reserved. This value can be one or more of the following values. Otherwise, mouseData is not used. 
            ///XBUTTON1
            ///The first X button was pressed or released.
            ///XBUTTON2
            ///The second X button was pressed or released.
            /// </summary>
            public int mouseData;
            /// <summary>
            /// Specifies the event-injected flag. An application can use the following value to test the mouse flags. Value Purpose 
            ///LLMHF_INJECTED Test the event-injected flag.  
            ///0
            ///Specifies whether the event was injected. The value is 1 if the event was injected; otherwise, it is 0.
            ///1-15
            ///Reserved.
            /// </summary>
            public int flags;
            /// <summary>
            /// Specifies the time stamp for this message.
            /// </summary>
            public int time;
            /// <summary>
            /// Specifies extra information associated with the message. 
            /// </summary>
            public int dwExtraInfo;
        }

        //Declare wrapper managed KeyboardHookStruct class.
        [StructLayout(LayoutKind.Sequential)]
        public class KeyboardHookStruct
        {
            public int vkCode;	//Specifies a virtual-key code. The code must be a value in the range 1 to 254. 
            public int scanCode; // Specifies a hardware scan code for the key. 
            public int flags;  // Specifies the extended-key flag, event-injected flag, context code, and transition-state flag.
            public int time; // Specifies the time stamp for this message.
            public int dwExtraInfo; // Specifies extra information associated with the message. 
        }

        public enum SetWindowPosInserAfterFlags : int
        {
            HWND_TOP = 0,
            HWND_TOPMOST = -1,
            HWND_NOTOPMOST = -2,
            HWND_BOTTOM = 1
        }

        public enum SetWindowPosFlags : uint
        {
            SWP_NOSIZE = 0x0001,
            SWP_NOMOVE = 0x0002,
            SWP_NOZORDER = 0x0004,
            SWP_NOACTIVATE = 0x0010,
            SWP_FRAMECHANGED = 0x0020,  /* The frame changed: send WM_NCCALCSIZE */
            SWP_SHOWWINDOW = 0x0040,
            SWP_HIDEWINDOW = 0x0080,
            SWP_NOOWNERZORDER = 0x0200,  /* Don't do owner Z ordering */

            SWP_DRAWFRAME = SWP_FRAMECHANGED,
            SWP_NOREPOSITION = SWP_NOOWNERZORDER
        }

        public enum HookType : int
        {
            WH_JOURNALRECORD = 0,
            WH_JOURNALPLAYBACK = 1,
            WH_KEYBOARD = 2,
            WH_GETMESSAGE = 3,
            WH_CALLWNDPROC = 4,
            WH_CBT = 5,
            WH_SYSMSGFILTER = 6,
            WH_MOUSE = 7,
            WH_HARDWARE = 8,
            WH_DEBUG = 9,
            WH_SHELL = 10,
            WH_FOREGROUNDIDLE = 11,
            WH_CALLWNDPROCRET = 12,
            WH_KEYBOARD_LL = 13,
            WH_MOUSE_LL = 14
        }

        public delegate bool CallBackPtr(IntPtr hWnd, uint lUserData);

        // A delegate for the enumeration callback
        public delegate bool WindowEnumDelegate(IntPtr hwnd, int lParam);

        // An enumerated integer holding the state for the message timeouts
        public enum SendMessageTimeoutFlags : uint
        {
            SMTO_NORMAL = 0x0000,
            SMTO_BLOCK = 0x0001,
            SMTO_ABORTIFHUNG = 0x0002,
            SMTO_NOTIMEOUTIFNOTHUNG = 0x0008
        }

        // Method import needed to ensure program timeout while waiting for ObjectFromLresult
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(IntPtr hWnd, uint Msg, UIntPtr wParam, UIntPtr lParam, SendMessageTimeoutFlags fuFlags, uint uTimeout, out UIntPtr lpdwResult);

        // Used to register WM_HTML_GETOBJECT
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern uint RegisterWindowMessage(string lpString);

        // Enumerates child windows
        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hwnd, WindowEnumDelegate del, int lParam);

        // Method which receives messages sent from a specified window
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, StringBuilder bld);

        [DllImport("user32.dll")]
        public static extern bool ExitWindowsEx(uint flags, uint reason);

        [DllImport("user32.dll")]
        public static extern IntPtr GetShellWindow();

        [DllImport("User32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, int cmd);

        [DllImport("User32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rect screenRect);

        [DllImport("User32.dll")]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool BringWindowToTop(IntPtr nativeWindow);

        [DllImport("USER32.DLL")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("User32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ShowWindow(IntPtr nativeWindow, Int32 cmdShow);

        [DllImport("user32.dll")]
        public static extern bool EnumDesktopWindows(IntPtr hDesktop, CallBackPtr callPtr, uint lUserData);

        [DllImport("user32.dll")]
        public static extern IntPtr GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetThreadDesktop(IntPtr dwThreadId);

        [DllImport("user32.dll")]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref WINDOWINFO pwi);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hDlg, uint Msg, UIntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hDlg, uint Msg, UIntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static public extern uint WaitForInputIdle(IntPtr hProcess, uint dwMilliseconds);

        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);

        //Import for SetWindowsHookEx function.
        //Use this function to install a hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);

        //Import for UnhookWindowsHookEx.
        //Call this function to uninstall the hook.
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        //Import for CallNextHookEx.
        //Use this function to pass the hook information to next hook procedure in chain.
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);

        //The ToAscii function translates the specified virtual-key code and keyboard state to the corresponding character or characters. The function translates the code using the input language and physical keyboard layout identified by the keyboard layout handle.
        [DllImport("user32")]
        public static extern int ToAscii(int uVirtKey, //[in] Specifies the virtual-key code to be translated. 
                                         int uScanCode, // [in] Specifies the hardware scan code of the key to be translated. The high-order bit of this value is set if the key is up (not pressed). 
                                         byte[] lpbKeyState, // [in] Pointer to a 256-byte array that contains the current keyboard state. Each element (byte) in the array contains the state of one key. If the high-order bit of a byte is set, the key is down (pressed). The low bit, if set, indicates that the key is toggled on. In this function, only the toggle bit of the CAPS LOCK key is relevant. The toggle state of the NUM LOCK and SCROLL LOCK keys is ignored.
                                         byte[] lpwTransKey, // [out] Pointer to the buffer that receives the translated character or characters. 
                                         int fuState); // [in] Specifies whether a menu is active. This parameter must be 1 if a menu is active, or 0 otherwise. 

        //The GetKeyboardState function copies the status of the 256 virtual keys to the specified buffer. 
        [DllImport("user32")]
        public static extern int GetKeyboardState(byte[] pbKeyState);

        [DllImport("user32.dll")]
        public static extern uint GetMessagePos();

        [DllImport("User32.dll")]
        public static extern IntPtr GetDC(HandleRef hWnd);

        [DllImport("User32.dll")]
        public static extern int ReleaseDC(HandleRef hWnd, HandleRef hDC);

        public enum DeviceCaps
        {
            /// <summary>
            /// Logical pixels inch in X
            /// </summary>
            LOGPIXELSX = 88,
        }

        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr hIcon);

        [DllImport("user32", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern int CallWindowProc(IntPtr lpPrevWndFunc, IntPtr hWnd, int Msg, int wParam, int lParam);

        [DllImport("user32", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr SetWindowLong(IntPtr hWnd, int nIndex, IntPtr newProc);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, ref StringBuilder lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern void SendMessage(HandleRef hWnd, uint msg, IntPtr wParam, ref tagRECT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(HandleRef hWnd, uint msg, IntPtr wParam, ref tagPOINT lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint MessageBox(IntPtr hWnd, String text, String caption, uint type);

        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hWnd, out tagRECT lpRect);

        [DllImport("user32.dll")]
        public static extern bool IsWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint uCmd);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, IntPtr windowTitle);

        [DllImport("user32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder title, int size);

        [DllImport("user32.dll")]
        public static extern uint RealGetWindowClass(IntPtr hWnd, StringBuilder pszType, uint cchType);

        [DllImport("user32.dll")]
        public static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern bool CopyRect([In, Out, MarshalAs(UnmanagedType.Struct)] ref tagRECT lprcDst, [In, MarshalAs(UnmanagedType.Struct)] ref tagRECT lprcSrc);

        [DllImport("user32.dll")]
        public static extern short GetKeyState(int nVirtKey);

        public delegate bool EnumThreadDelegate(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern bool EnumThreadWindows(int dwThreadId, EnumThreadDelegate lpfn, IntPtr lParam);

        #endregion

        #region GDI32.dll

        [DllImport("GDI32.dll")]
        public static extern int GetDeviceCaps(HandleRef hDC, int nIndex);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hdc, int nWidth, int nHeight);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteObject(IntPtr hObject);

        public enum StretchMode
        {
            STRETCH_ANDSCANS = 1,
            STRETCH_ORSCANS = 2,
            STRETCH_DELETESCANS = 3,
            STRETCH_HALFTONE = 4
        }

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool SetStretchBltMode(IntPtr hdc, StretchMode iStretchMode);

        public enum TernaryRasterOperations
        {
            SRCCOPY = unchecked(0x00CC0020), /* dest = source*/
            SRCPAINT = unchecked(0x00EE0086), /* dest = source OR dest*/
            SRCAND = unchecked(0x008800C6), /* dest = source AND dest*/
            SRCINVERT = unchecked(0x00660046), /* dest = source XOR dest*/
            SRCERASE = unchecked(0x00440328), /* dest = source AND (NOT dest )*/
            NOTSRCCOPY = unchecked(0x00330008), /* dest = (NOT source)*/
            NOTSRCERASE = unchecked(0x001100A6), /* dest = (NOT src) AND (NOT dest) */
            MERGECOPY = unchecked(0x00C000CA), /* dest = (source AND pattern)*/
            MERGEPAINT = unchecked(0x00BB0226), /* dest = (NOT source) OR dest*/
            PATCOPY = unchecked(0x00F00021), /* dest = pattern*/
            PATPAINT = unchecked(0x00FB0A09), /* dest = DPSnoo*/
            PATINVERT = unchecked(0x005A0049), /* dest = pattern XOR dest*/
            DSTINVERT = unchecked(0x00550009), /* dest = (NOT dest)*/
            BLACKNESS = unchecked(0x00000042), /* dest = BLACK*/
            WHITENESS = unchecked(0x00FF0062) /* dest = WHITE*/
        }

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjSource, int nXSrc, int nYSrc, TernaryRasterOperations dwRop);

        [DllImport("gdi32.dll", SetLastError = true)]
        public static extern bool StretchBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidthDest, int nHeightDest, IntPtr hdcSrc, int nXSrc, int nYSrc, int nWidthSrc, int nHeightSrc, TernaryRasterOperations dwRop);

        #endregion

        # region wininet.dll

        [StructLayout(LayoutKind.Explicit, Size = 80, CharSet = CharSet.Auto)]
        public struct INTERNETCACHEENTRYINFO
        {
            [FieldOffset(0)]
            public uint dwStructSize;
            [FieldOffset(4)]
            public IntPtr lpszSourceUrlName;
            [FieldOffset(8)]
            public IntPtr lpszLocalFileName;
            [FieldOffset(12)]
            public uint CacheEntryType;
            [FieldOffset(16)]
            public uint dwUseCount;
            [FieldOffset(20)]
            public uint dwHitRate;
            [FieldOffset(24)]
            public uint dwSizeLow;
            [FieldOffset(28)]
            public uint dwSizeHigh;
            [FieldOffset(32)]
            public long LastModifiedTime;
            [FieldOffset(40)]
            public long ExpireTime;
            [FieldOffset(48)]
            public long LastAccessTime;
            [FieldOffset(56)]
            public long LastSyncTime;
            [FieldOffset(64)]
            public IntPtr lpHeaderInfo;
            [FieldOffset(68)]
            public uint dwHeaderInfoSize;
            [FieldOffset(72)]
            public IntPtr lpszFileExtension;
            [FieldOffset(76)]
            public uint dwReserved;
            [FieldOffset(76)]
            public uint dwExemptDelta;
        }
        [DllImport("wininet.dll", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr FindFirstUrlCacheEntry([MarshalAs(UnmanagedType.LPTStr)] string lpszUrlSearchPattern, IntPtr lpFirstCacheEntryInfo, ref int lpdwFirstCacheEntryInfoBufferSize);

        [DllImport("wininet.dll", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool FindNextUrlCacheEntry(IntPtr hFind, IntPtr lpNextCacheEntryInfo, ref int lpdwNextCacheEntryInfoBufferSize);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetGetCookie(string lpszUrlName, string lpszCookieName, StringBuilder lpszCookieData, [MarshalAs(UnmanagedType.U4)]ref int lpdwSize);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string lpszUrlName, string lpszCookieName, string lpszCookieData);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetOption(int hInternet, int dwOption, string lpBuffer, int dwBufferLength);

        [DllImport("wininet", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool DeleteUrlCacheEntry(IntPtr lpszUrlName);

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern long FindCloseUrlCache(IntPtr hEnumHandle);

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern IntPtr FindFirstUrlCacheEntry(string lpszUrlSearchPattern, IntPtr lpFirstCacheEntryInfo, out UInt32 lpdwFirstCacheEntryInfoBufferSize);

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern long FindNextUrlCacheEntry(IntPtr hEnumHandle, IntPtr lpNextCacheEntryInfo, out UInt32 lpdwNextCacheEntryInfoBufferSize);

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern bool GetUrlCacheEntryInfo(string lpszUrlName, IntPtr lpCacheEntryInfo, out UInt32 lpdwCacheEntryInfoBufferSize);

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern long DeleteUrlCacheEntry(string lpszUrlName);

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern IntPtr RetrieveUrlCacheEntryStream(string lpszUrlName, IntPtr lpCacheEntryInfo, out UInt32 lpdwCacheEntryInfoBufferSize, long fRandomRead, UInt32 dwReserved);

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern IntPtr ReadUrlCacheEntryStream(IntPtr hUrlCacheStream, UInt32 dwLocation, IntPtr lpBuffer, out UInt32 lpdwLen, UInt32 dwReserved);

        [DllImport("wininet.dll", SetLastError = true)]
        public static extern long UnlockUrlCacheEntryStream(IntPtr hUrlCacheStream, UInt32 dwReserved);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, IntPtr pvParam, uint fWinIni);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetOption(IntPtr hInternet, int dwOption, IntPtr lpBuffer, int lpdwBufferLength);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetGetCookie(string lpszUrlName, string lpszCookieName, [Out] string lpszCookieData, [MarshalAs(UnmanagedType.U4)] out int lpdwSize);

        [DllImport("wininet.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool InternetSetCookie(string lpszUrlName, string lpszCookieName, IntPtr lpszCookieData);

        [Flags]
        public enum InternetConnectionState_e : int
        {
            INTERNET_CONNECTION_MODEM = 0x1,
            INTERNET_CONNECTION_LAN = 0x2,
            INTERNET_CONNECTION_PROXY = 0x4,
            INTERNET_RAS_INSTALLED = 0x10,
            INTERNET_CONNECTION_OFFLINE = 0x20,
            INTERNET_CONNECTION_CONFIGURED = 0x40
        }

        [DllImport("wininet.dll", CharSet = CharSet.Auto)]
        public extern static bool InternetGetConnectedState(ref InternetConnectionState_e lpdwFlags, int dwReserved);

        #endregion

        #region shdocvw.dll

        [DllImport("shdocvw.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        public static extern long DoOrganizeFavDlg(long hWnd, string lpszRootFolder);

        [DllImport("shdocvw.dll", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
        public static extern long AddUrlToFavorites(long hwnd, [MarshalAs(UnmanagedType.LPWStr)] string pszUrlW, [MarshalAs(UnmanagedType.LPWStr)] string pszTitleW, [MarshalAs(UnmanagedType.Bool)] bool fDisplayUI);

        #endregion

        #region OLE32.dll

        [DllImport("ole32.dll", SetLastError = true,
            ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int RevokeObjectParam([In, MarshalAs(UnmanagedType.LPWStr)] string pszKey);

        [DllImport("ole32.dll", CharSet = CharSet.Auto)]
        public static extern int CreateStreamOnHGlobal(IntPtr hGlobal, bool fDeleteOnRelease, [MarshalAs(UnmanagedType.Interface)] out IStream ppstm);

        public enum CLSCTX
        {
            CLSCTX_INPROC_SERVER = 0x1,
            CLSCTX_INPROC_HANDLER = 0x2,
            CLSCTX_LOCAL_SERVER = 0x4,
            CLSCTX_INPROC_SERVER16 = 0x8,
            CLSCTX_REMOTE_SERVER = 0x10,
            CLSCTX_INPROC_HANDLER16 = 0x20,
            CLSCTX_RESERVED1 = 0x40,
            CLSCTX_RESERVED2 = 0x80,
            CLSCTX_RESERVED3 = 0x100,
            CLSCTX_RESERVED4 = 0x200,
            CLSCTX_NO_CODE_DOWNLOAD = 0x400,
            CLSCTX_RESERVED5 = 0x800,
            CLSCTX_NO_CUSTOM_MARSHAL = 0x1000,
            CLSCTX_ENABLE_CODE_DOWNLOAD = 0x2000,
            CLSCTX_NO_FAILURE_LOG = 0x4000,
            CLSCTX_DISABLE_AAA = 0x8000,
            CLSCTX_ENABLE_AAA = 0x10000,
            CLSCTX_FROM_DEFAULT_CONTEXT = 0x20000,
            CLSCTX_INPROC = CLSCTX_INPROC_SERVER | CLSCTX_INPROC_HANDLER,
            CLSCTX_SERVER = CLSCTX_INPROC_SERVER | CLSCTX_LOCAL_SERVER | CLSCTX_REMOTE_SERVER,
            CLSCTX_ALL = CLSCTX_SERVER | CLSCTX_INPROC_HANDLER
        }

        [DllImport("ole32.dll", ExactSpelling = true, PreserveSig = false)]
        [return: MarshalAs(UnmanagedType.Interface)]
        public static extern object CoCreateInstance([In, MarshalAs(UnmanagedType.LPStruct)] Guid rclsid, [MarshalAs(UnmanagedType.IUnknown)] object pUnkOuter, CLSCTX dwClsContext, [In, MarshalAs(UnmanagedType.LPStruct)] Guid riid);

        //MessageBox(new IntPtr(0), "Text", "Caption", 0 );
        //[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        //public static extern uint RegisterClipboardFormat(string lpszFormat);

        [DllImport("ole32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int CreateBindCtx([MarshalAs(UnmanagedType.U4)] uint dwReserved, [Out, MarshalAs(UnmanagedType.Interface)] out IBindCtx ppbc);

        #endregion

        #region urlmon.dll

        [DllImport("urlmon.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int CreateURLMoniker([MarshalAs(UnmanagedType.Interface)] IMoniker pmkContext, [MarshalAs(UnmanagedType.LPWStr)] string szURL, [Out, MarshalAs(UnmanagedType.Interface)] out IMoniker ppmk);

        public const uint URL_MK_LEGACY = 0;
        public const uint URL_MK_UNIFORM = 1;
        public const uint URL_MK_NO_CANONICALIZE = 2;

        [DllImport("urlmon.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        public static extern int CreateURLMonikerEx([MarshalAs(UnmanagedType.Interface)] IMoniker pmkContext, [MarshalAs(UnmanagedType.LPWStr)] string szURL, [Out, MarshalAs(UnmanagedType.Interface)] out IMoniker ppmk, uint URL_MK_XXX); //Flags, one of 

        #endregion

        #region  Wtsapi32.dll

        public enum WTS_CONNECTSTATE_CLASS
        {
            WTSActive,
            WTSConnected,
            WTSConnectQuery,
            WTSShadow,
            WTSDisconnected,
            WTSIdle,
            WTSListen,
            WTSReset,
            WTSDown,
            WTSInit
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WTS_SESSION_INFO
        {
            public Int32 SessionID;
            [MarshalAs(UnmanagedType.LPStr)]
            public String pWinStationName;

            public WTS_CONNECTSTATE_CLASS State;
        }

        [DllImport("Wtsapi32.dll")]
        public static extern bool WTSQuerySessionInformationW(IntPtr hServer, int SessionId, int WTSInfoClass, out IntPtr ppBuffer, out IntPtr pBytesReturned);

        [DllImport("wtsapi32.dll", EntryPoint = "WTSQueryUserToken", SetLastError = true, CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool WTSQueryUserToken(ulong SessionId, ref IntPtr phToken);

        [DllImport("wtsapi32.dll", EntryPoint = "WTSFreeMemory", SetLastError = false)]
        public static extern void WTSFreeMemory(IntPtr memory);

        [DllImport("Wtsapi32.dll", EntryPoint = "WTSQuerySessionInformation", SetLastError = true)]
        public static extern bool WTSQuerySessionInformation(IntPtr hServer, int sessionId, WTS_INFO_CLASS wtsInfoClass, out IntPtr ppBuffer, out uint pBytesReturned);

        [DllImport("wtsapi32.dll", EntryPoint = "WTSQueryUserToken", SetLastError = true)]
        public static extern bool WTSQueryUserToken(UInt32 in_nSessionID, out IntPtr out_ptrTokenHandle);

        [DllImport("wtsapi32.dll")]
        public static extern Int32 WTSEnumerateSessions(IntPtr hServer, [MarshalAs(UnmanagedType.U4)] Int32 Reserved, [MarshalAs(UnmanagedType.U4)] Int32 Version, ref IntPtr ppSessionInfo, [MarshalAs(UnmanagedType.U4)] ref Int32 pCount);

        #endregion

        #region  msi.dll
        [DllImport("msi.dll")]
        public static extern Int32 MsiQueryProductState(string szProduct);
        #endregion

        #region oleacc.dll

        // Method import to ensure correct casting of IHTMLDOCUMENT2 class type
        [DllImport("oleacc.dll", PreserveSig = false)]
        [return: MarshalAs(UnmanagedType.Interface)]
        public static extern object ObjectFromLresult(UIntPtr lResult, [MarshalAs(UnmanagedType.LPStruct)] Guid refiid, IntPtr wParam);

        #endregion

        #region userenv.dll

        [DllImport("userenv.dll", EntryPoint = "CreateEnvironmentBlock", SetLastError = true)]
        public static extern bool CreateEnvironmentBlock(out IntPtr out_ptrEnvironmentBlock, IntPtr in_ptrTokenHandle, bool in_bInheritProcessEnvironment);

        [DllImport("userenv.dll", EntryPoint = "LoadUserProfile", SetLastError = true)]
        public static extern bool LoadUserProfile(IntPtr hToken, ref PROFILEINFO lpProfileInfo);

        #endregion

        #region "SERVICE RECOVERY INTEROP"

        // ReSharper disable InconsistentNaming
        public enum SC_ACTION_TYPE
        {
            None = 0,
            RestartService = 1,
            RebootComputer = 2,
            RunCommand = 3
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SC_ACTION
        {
            public SC_ACTION_TYPE Type;
            public uint Delay;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public struct SERVICE_FAILURE_ACTIONS
        {
            public int dwResetPeriod;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpRebootMsg;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string lpCommand;
            public int cActions;
            public IntPtr lpsaActions;
        }

        public const int SERVICE_CONFIG_FAILURE_ACTIONS = 2;

        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool ChangeServiceConfig2(IntPtr hService, int dwInfoLevel, IntPtr lpInfo);

        [DllImport("advapi32.dll", EntryPoint = "QueryServiceConfig2", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern int QueryServiceConfig2(IntPtr hService, int dwInfoLevel, IntPtr lpBuffer, uint cbBufSize, out uint pcbBytesNeeded);

        // ReSharper restore InconsistentNaming
        #endregion

        #region Netapi32.dll

        [DllImport("Netapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public extern static int NetStatisticsGet(
            [MarshalAs(UnmanagedType.LPWStr)] string serverName,
            [MarshalAs(UnmanagedType.LPWStr)] string service,
            int level,
            int options,
            out IntPtr BufPtr);

        #endregion
    }
}
