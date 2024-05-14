using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Styling;
using DynamicData.Tests;
using ReactiveUI;
using OsuData;
using System.Timers;
using OsuData.FileExplainer;
using Avalonia.Media.Imaging;
using System.Threading.Tasks;
using System.Net.Http;

namespace AvaloniaStyleWindow.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            PlayList = osuDataManager.DotOsuFileInfoDictionary.Keys.ToList();
            PlayingItemIndex = 0;
            DisPlayingItemIndex = PlayingItemIndex;
            PlayingSongData = new(osuDataManager.DotOsuFileInfoDictionary[PlayList[PlayingItemIndex].ToString()]);
            DisPlayingSongData = PlayingSongData;
            LoadCover();
        }
        public List<string>? _PlayList;
        public List<string>? PlayList
        {
            get => _PlayList;
            set => this.RaiseAndSetIfChanged(ref _PlayList, value);
        }
        private int PlayingItemIndex;
        private DotOsuReader PlayingSongData;
        
        private int DisPlayingItemIndex;
        private DotOsuReader DisPlayingSongData;
        public string DisPlayingPictureDirs
        {
            get => DisPlayingSongData.imgDir??"null";
        }

        OsuDataManager osuDataManager = new(@"E:\SmallGame\osu", @"C:\Users\Alyce\Desktop\test");

        private Bitmap? _cover;
        public Bitmap? Cover
        {
            get => _cover;
            private set => this.RaiseAndSetIfChanged(ref _cover, value);
        }
        public async Task<Stream> LoadCoverBitmapAsync()
        {
            if (File.Exists(DisPlayingSongData.imgDir + ".jpg"))
            {
                return File.OpenRead(DisPlayingSongData.imgDir + ".jpg");
            }
            else if(File.Exists(DisPlayingSongData.imgDir + ".png"))
            {
                return File.OpenRead(DisPlayingSongData.imgDir + ".png");
            }
            else
            {
                // do nothing
                return null;
            }
        }
        public async Task LoadCover()
        {
            await using (var imageStream = await LoadCoverBitmapAsync())
            {
                Cover = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
            }
        }
    }
}
