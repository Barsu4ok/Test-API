using System;
using System.Threading;
using static Test_API.B322_api;
using static Test_API.B322_config;

namespace Test_API
{
    public class Program
    {
        public static double[] data_channel_a;
        public static double[] data_channel_b;
        static void Main(string[] args)
        {
            var isRunning = true;
            Console.WriteLine("Start program:");

            var device = b322_init(null);
            if (device == IntPtr.Zero)
            {
                Console.WriteLine("failed to init device\n");
                return;
            }

            while (isRunning)
            {
               
                b322_set_callback_status(device, callback_status);

                if (b322_connect_usb(device) == 0)
                {
                    Console.WriteLine("failed to connect via USB\n");
                    break;
                }

                Console.WriteLine($"device PId = " + b322_get_BoardPId(device));
                Console.WriteLine($"device SN = " + b322_get_serialnumber(device));

                var config = initialize();
                config.acq_frequency = 100000000;
                config.acq_mode = (int)b322_config_mode_e.B322_CONFIG_MODE_OSCILLOSCOPE;
                config.acq_history = 0;
                config.trigger_type = (uint)b322_config_trigger_type_e.B322_CONFIG_TRIGGER_TYPE_AUTO;
                config.trigger_level = 0.0;
                config.trigger_filter = (uint)b322_config_filter_e.B322_CONFIG_FILTER_NOFILTER;
                config.trigger_slope = (uint)b322_config_slope_e.B322_CONFIG_SLOPE_NEGATIVE;
                config.trigger_external = 0;
                config.trigger_external_attenuation = 1.0;
                config.trigger_external_coupling = (uint)b322_config_coupling_e.B322_CONFIG_COUPLING_DC;
                config.trigger_internal_channel = (uint)b322_config_channel_e.B322_CONFIG_TRIGGER_CHANNEL_B;

                if (b322_config_measurement(device, ref config) == 0)
                {
                    Console.WriteLine("failed to configure device\n");
                    break;
                }

                var samples = new UIntPtr(512);
                data_channel_a = new double[samples.ToUInt32()];
                data_channel_b = new double[samples.ToUInt32()];

                if (b322_start_measurement(device, data_channel_a, data_channel_b, samples) == 0)
                {
                    Console.WriteLine("failed to start measurement\n");
                    break;
                }

                Thread.Sleep(3000);

                if (b322_stop_measurement(device) == 0)
                {
                    Console.WriteLine("failed to stop measurement\n");
                    break;
                }
                
                Console.WriteLine("Enter Yes if you want to restart or No if you want to end it");
                var key = Console.ReadLine().ToLower();
                if (key == "no")
                {
                    isRunning = false;
                }
            }

            b322_deinit(device);
            Console.WriteLine("End program!");
            Console.ReadKey();

        }

        static void callback_status(IntPtr device, IntPtr cookie, b322_status_e status)
        {
            switch (status)
            {
                case b322_status_e.B322_STATUS_MEAS_RUNNING:
                    Console.WriteLine("B322_CALLBACK -> MEAS_RUNNING\n");
                    break;
                case b322_status_e.B322_STATUS_MEAS_DATA:
                    Console.WriteLine("B322_CALLBACK -> MEAS_DATA\n");
                    var chMin = new double[B322_CONFIG_CHANNEL_COUNT];
                    var chMax = new double[B322_CONFIG_CHANNEL_COUNT];

                    foreach (var sample in data_channel_a)
                    {
                        chMin[(uint)b322_config_channel_e.B322_CONFIG_TRIGGER_CHANNEL_A] = Math.Min(chMin[(uint)b322_config_channel_e.B322_CONFIG_TRIGGER_CHANNEL_A], sample);
                        chMax[(uint)b322_config_channel_e.B322_CONFIG_TRIGGER_CHANNEL_A] = Math.Max(chMax[(uint)b322_config_channel_e.B322_CONFIG_TRIGGER_CHANNEL_A], sample);
                    }

                    foreach (var sample in data_channel_b)
                    {
                        chMin[(uint)b322_config_channel_e.B322_CONFIG_TRIGGER_CHANNEL_B] = Math.Min(chMin[(uint)b322_config_channel_e.B322_CONFIG_TRIGGER_CHANNEL_B], sample);
                        chMax[(uint)b322_config_channel_e.B322_CONFIG_TRIGGER_CHANNEL_B] = Math.Max(chMax[(uint)b322_config_channel_e.B322_CONFIG_TRIGGER_CHANNEL_B], sample);
                    }

                    Console.WriteLine($"- ch A: count = {data_channel_a.Length}");
                    for (var i = 0; i < data_channel_a.Length; i++)
                    {
                        Console.Write($"{i} : {data_channel_a[i]}, ");
                    }
                    Console.WriteLine($"\n- ch B: count = {data_channel_a.Length}");
                    for (var i = 0; i < data_channel_a.Length; i++)
                    {
                        Console.Write($"{i} : {data_channel_b[i]}, ");
                    }

                    B322_api.b322_stop_measurement(device);

                    break;
                case b322_status_e.B322_STATUS_MEAS_ABORT:
                    Console.WriteLine("B322_CALLBACK -> MEAS_ABORT\n");
                    break;
                case b322_status_e.B322_STATUS_MEAS_ERROR:
                    Console.WriteLine("B322_CALLBACK -> MEAS_ERROR\n");
                    break;
                default:
                    Console.WriteLine("B322_CALLBACK -> UNKNOWN\n");
                    break;

            }
        }

        static b322_config_t initialize()
        {
            var config = new b322_config_t
            {
                channel_enable = new byte[2] { 1, 1 },
                channel_coupling = new uint[2] { (uint)b322_config_coupling_e.B322_CONFIG_COUPLING_DC, (uint)b322_config_coupling_e.B322_CONFIG_COUPLING_DC },
                channel_gain = new uint[2] { (uint)b322_config_gain_e.B322_CONFIG_GAIN_4000MV, (uint)b322_config_gain_e.B322_CONFIG_GAIN_4000MV },
                channel_probe_attenuation = new double[2] { 1.0, 1.0 },
                channel_zero_offset = new double[2] { 0, 0 }
            };
            return config;
        }
    }
}
