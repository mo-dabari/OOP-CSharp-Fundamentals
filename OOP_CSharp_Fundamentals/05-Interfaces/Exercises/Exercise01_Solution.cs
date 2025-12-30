using System;
using System.Collections.Generic;
using System.Linq;

namespace Interfaces.Exercises
{
    public interface IPlayable
    {
        void Play();
        void Pause();
        void Stop();
        string GetTitle();
        double GetDuration();
    }

    public class Song : IPlayable
    {
        private string _title;
        private double _duration;

        public Song(string title, double duration)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(title, nameof(title));
            ArgumentOutOfRangeException.ThrowIfNegativeOrZero(duration, nameof(duration));

            _title = title;
            _duration = duration;
        }

        public void Play()
        {
            Console.WriteLine($"🎵 تشغيل الأغنية: {_title}");
        }
        public void Pause()
        {
            Console.WriteLine($"⏸️  إيقاف الأغنية: {_title}");
        }
        public void Stop()
        {
            Console.WriteLine($"⏹️  إيقاف الأغنية: {_title}");
        }
        public string GetTitle() => _title;
        public double GetDuration() => _duration;
    }

    public class Podcast : IPlayable
    {
        private string title;
        private int duration;

        public Podcast(string title, int duration)
        {
            this.title = title;
            this.duration = duration;
        }

        public void Play()
        {
            Console.WriteLine($"🎙️  تشغيل البودكاست: {title}");
        }

        public void Pause()
        {
            Console.WriteLine($"⏸️  إيقاف البودكاست");
        }

        public void Stop()
        {
            Console.WriteLine($"⏹️  إيقاف البودكاست");
        }

        public string GetTitle() => title;
        public double GetDuration() => duration;
    }

    public class Audiobook : IPlayable
    {
        private string title;
        private int duration;

        public Audiobook(string title, int duration)
        {
            this.title = title;
            this.duration = duration;
        }

        public void Play()
        {
            Console.WriteLine($"📚 تشغيل الكتاب الصوتي: {title}");
        }

        public void Pause()
        {
            Console.WriteLine($"⏸️  إيقاف الكتاب");
        }

        public void Stop()
        {
            Console.WriteLine($"⏹️  إيقاف الكتاب");
        }

        public string GetTitle() => title;
        public double GetDuration() => duration;
    }

    public class MusicPlayer
    {
        private List<IPlayable> _playlist = new();

        public IReadOnlyList<IPlayable> Playlist;

        public MusicPlayer()
        {
            Playlist = _playlist.AsReadOnly();
        }
        public void AddTrack(IPlayable track)
        {
            _playlist.Add(track);
            Console.WriteLine($"✅ تمت إضافة: {track.GetTitle()}");
        }

        public void PlayAll()
        {
            Console.WriteLine("\n▶️  تشغيل جميع المسارات:");
            foreach (var track in Playlist)
            {
                track.Play();
            }
        }

        public void PrintPlaylist()
        {
            Console.WriteLine("\n📋 قائمة التشغيل:");
            double totalDuration = 0;
            for (int i = 0; i < Playlist.Count; i++)
            {
                var track = Playlist[i];
                Console.WriteLine($"{i + 1}. {track.GetTitle()} ({track.GetDuration()} ثانية)");
                totalDuration += track.GetDuration();
            }
            Console.WriteLine($"الوقت الإجمالي: {totalDuration} ثانية");
        }
    }
}
