using System;
using System.Runtime.InteropServices;

namespace Test_API
{
    public class B322_api
    {
        public const int RETURN_OK = 0; // succeeded

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void b322_callback_status(IntPtr device, IntPtr cookie, B322_config.b322_status_e status);

        [DllImport("Lib\\b322-device.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern IntPtr b322_init(b322_callback_status status);

        [DllImport("Lib\\b322-device.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void b322_deinit(IntPtr dev_ctx);
        [DllImport("Lib\\b322-device.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void b322_set_callback(IntPtr dev_ctx, b322_callback_status callbacks);
        [DllImport("Lib\\b322-device.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void b322_set_callback_cookie(IntPtr dev_ctx, IntPtr cookie);

        [DllImport("Lib\\b322-device.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern void b322_set_callback_status(IntPtr dev_ctx, b322_callback_status callback_status);
        [DllImport("Lib\\b322-device.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern byte b322_connect_network(IntPtr dev_ctx, uint ip, ushort port);
        [DllImport("Lib\\b322-device.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern byte b322_connect_usb(IntPtr dev_ctx);
        [DllImport("Lib\\b322-device.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern byte b322_start_measurement(IntPtr dev_ctx, double[] channel_1_buf, double[] channel_2_buf, UIntPtr samples_count);
        [DllImport("Lib\\b322-device.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern byte b322_stop_measurement(IntPtr dev_ctx);
        [DllImport("Lib\\b322-device.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ushort b322_get_serialnumber(IntPtr dev_ctx);
        [DllImport("Lib\\b322-device.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern ushort b322_get_BoardPId(IntPtr dev_ctx);
        [DllImport("Lib\\b322-device.dll", CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
        public static extern byte b322_config_measurement(IntPtr dev_ctx,  ref B322_config.b322_config_t config);
    }
}
