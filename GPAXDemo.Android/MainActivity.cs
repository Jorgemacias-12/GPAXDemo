using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Media;
using Android.Widget;
using GPAXDemo.Droid.Models;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Android;

namespace GPAXDemo.Droid
{
    [Activity(Label = "GPAXDemo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private AudioRecorder Recorder = AudioRecorder.Instance;
        private Button Record, Stop, Play;
        private const int RequestRecordAudioPermissionCode = 100;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.main);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App());

            CheckPermissions();

            Record = FindViewById<Button>(Resource.Id.start_record);
            Stop   = FindViewById<Button>(Resource.Id.stop_record);
            Play   = FindViewById<Button>(Resource.Id.play_record);

            Record.Click += Record_Click;
            Stop.Click += Stop_Click;
            Play.Click += Play_Click;

            Recorder.AppContext = this;
        }

        private void Play_Click(object sender, EventArgs e)
        {
            Recorder.PlayRecording();
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            Recorder.StopRecording();   
        }

        private void Record_Click(object sender, EventArgs e)
        {
            Recorder.StartRecording();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void CheckPermissions()
        {
            string[] Permissions =
            {
                Manifest.Permission.RecordAudio,
                Manifest.Permission.WriteExternalStorage
            };

            foreach (string permission in Permissions)
            {
                if (ContextCompat.CheckSelfPermission(this, permission) != Permission.Granted)
                {
                    // Si no se tiene permiso, solicitarlo al usuario
                    ActivityCompat.RequestPermissions(this, new string[] { permission }, RequestRecordAudioPermissionCode);
                }
            }
        }
    }
}