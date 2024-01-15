using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Test_API
{
    public class B322_config
    {
        public static readonly uint B322_CONFIG_CHANNEL_COUNT = 2;
        //
        // Enums
        //
        public enum b322_status_e : int
        {
            B322_STATUS_UNKNOWN = 0,

            B322_STATUS_MEAS_RUNNING = 1,

            B322_STATUS_MEAS_DATA = 2,

            B322_STATUS_MEAS_ABORT = 3,

            B322_STATUS_MEAS_ERROR = 4,
        }
        public enum b322_config_coupling_e
        {
            B322_CONFIG_COUPLING_AC = 0,
            B322_CONFIG_COUPLING_DC = 1,
        }
        public enum b322_config_slope_e
        {
            B322_CONFIG_SLOPE_POSITIVE = 0,
            B322_CONFIG_SLOPE_NEGATIVE = 1,
        }
        public enum b322_config_filter_e
        {
            B322_CONFIG_FILTER_NOFILTER = 0,
            B322_CONFIG_FILTER_HFREJECTION = 1,
        }
        public enum b322_config_gain_e
        {
            B322_CONFIG_GAIN_40MV = 0,
            B322_CONFIG_GAIN_80MV = 1,
            B322_CONFIG_GAIN_160MV = 2,
            B322_CONFIG_GAIN_400MV = 3,
            B322_CONFIG_GAIN_800MV = 4,
            B322_CONFIG_GAIN_1600MV = 5,
            B322_CONFIG_GAIN_4000MV = 6,
            B322_CONFIG_GAIN_8000MV = 7,
            B322_CONFIG_GAIN_16000MV = 8,
            B322_CONFIG_GAIN_40000MV = 9,
        }
        public enum b322_config_mode_e
        {
            B322_CONFIG_MODE_OSCILLOSCOPE = 0,
        }
        public enum b322_config_channel_e
        {
            B322_CONFIG_TRIGGER_CHANNEL_A = 0,
            B322_CONFIG_TRIGGER_CHANNEL_B = 1,
        }
        public enum b322_config_trigger_type_e
        {
            B322_CONFIG_TRIGGER_TYPE_AUTO = 0,
            B322_CONFIG_TRIGGER_TYPE_NORMAL = 1,
        }
        /// <summary>Defines the type of a member as it was used in the native signature.</summary>
        [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = false, Inherited = true)]
        [Conditional("DEBUG")]
        internal sealed partial class NativeTypeNameAttribute : Attribute
        {
            private readonly string _name;

            /// <summary>Initializes a new instance of the <see cref="NativeTypeNameAttribute" /> class.</summary>
            /// <param name="name">The name of the type that was used in the native signature.</param>
            public NativeTypeNameAttribute(string name)
            {
                _name = name;
            }

            /// <summary>Gets the name of the type that was used in the native signature.</summary>
            public string Name => _name;
        }
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct b322_config_t
        {
            [NativeTypeName("uint32_t")]
            public uint acq_frequency;

            [NativeTypeName("uint32_t")]
            public uint acq_mode;

            [NativeTypeName("int32_t")]
            public int acq_history;

            [NativeTypeName("bool[2]")]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] channel_enable;

            [NativeTypeName("uint32_t[2]")]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public uint[] channel_coupling;

            [NativeTypeName("uint32_t[2]")]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public  uint[] channel_gain;

            [NativeTypeName("double[2]")]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public double[] channel_probe_attenuation;

            [NativeTypeName("double[2]")]
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public double[] channel_zero_offset;

            [NativeTypeName("uint32_t")]
            public uint trigger_type;

            public double trigger_level;

            [NativeTypeName("uint32_t")]
            public uint trigger_filter;

            [NativeTypeName("uint32_t")]
            public uint trigger_slope;

            [NativeTypeName("bool")]
            public byte trigger_external;

            public double trigger_external_attenuation;

            [NativeTypeName("uint32_t")]
            public uint trigger_external_coupling;

            [NativeTypeName("uint32_t")]
            public uint trigger_internal_channel;
        }
    }
}
