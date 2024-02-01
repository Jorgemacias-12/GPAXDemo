using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GPAXDemo.Droid.Models
{
    public class AudioRecorder
    {
        private static AudioRecorder _Instance;
        private MediaRecorder Recorder;
        private MediaPlayer Player;
        private string CurrentAudioFileName;
        public Context AppContext = null;

        private readonly string ApplicationFolderPath = Path.Combine(Application.Context.FilesDir.AbsolutePath.ToString(), "Recordings");

        public static AudioRecorder Instance
        {
            get
            {
                _Instance ??= new AudioRecorder();

                return _Instance;
            }
        }

        private string GenerateFileName()
        {
            string FormatExtension = ".mp4";

            string value = $"CodigoVioleta_${DateTime.Now:dd-mm-yyyy_hh:mm:ss}{FormatExtension}";

            string result = Path.Combine(ApplicationFolderPath, value);

            CurrentAudioFileName = result;

            return result;
        }

        private void CreateFolderIfNotPresent()
        {
            try
            {
                if (Directory.Exists(ApplicationFolderPath))
                {
                    return;
                }
                Directory.CreateDirectory(ApplicationFolderPath);

                //Toast.MakeText(AppContext, $"{Environment.GetExternalStoragePublicDirectory}", ToastLength.Short).Show();
            }
            catch(Exception ex)
            {
                Toast.MakeText(AppContext, $"{ex}", ToastLength.Short).Show();
            }
        }

        public void StartRecording()
        {
            if (Recorder != null)
            {
                StopRecording();
            }

            CreateFolderIfNotPresent();

            string File = GenerateFileName();

            Recorder = new MediaRecorder(AppContext);

            try
            {
                Recorder.SetAudioSource(AudioSource.Mic);
                Recorder.SetOutputFormat(OutputFormat.Mpeg4);
                Recorder.SetAudioEncoder(AudioEncoder.Aac);
                Recorder.SetOutputFile(File);
                Recorder.SetAudioEncodingBitRate(16);
                Recorder.SetAudioSamplingRate(44100);
                Recorder.Prepare();
                Recorder.Start();
            }
            catch (Exception ex)
            {
                Toast.MakeText(AppContext, $"{ex}", ToastLength.Short).Show();
            }
        }

        public void StopRecording()
        {
            if (Recorder is null) return;

            Recorder.Stop();
            Recorder.Release();
            Recorder.Dispose();

            Recorder = null;
        }

        public void PlayRecording()
        {
            if (Player != null)
            {
                Player.Release();
                Player = null;
            }

            try
            {
                Player = new MediaPlayer();

                if (CurrentAudioFileName == string.Empty) throw new Exception("No file name was generated");

                Player.Reset();
                Player.SetDataSource(CurrentAudioFileName);
                Player.Prepare();
                Player.Start();
            }
            catch(Exception ex)
            {
                Toast.MakeText(AppContext, $"{ex}", ToastLength.Short).Show();
            }
        }
    }
}