using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSMusicPlayer
{
    public partial class MusicPlayer : Form
    {
        //List<string> _songNames;
        //List<string> _songPaths;
        List<Song> _songs;

        public MusicPlayer()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            
            if(dialog.ShowDialog() == DialogResult.OK)
            {
                AddSongsToList(dialog.SafeFileNames.ToList(), dialog.FileNames.ToList());
                
                // Add son to list

            }
        }
        private void AddSongsToList(List<string> names, List<string> paths)
        {
            if(_songs == null)
                _songs = new List<Song>();
            
            
            foreach (var item in names)
            {
                if (!ExistOnList(item))
                    _songs.Add(new Song(item, GetPath(item, paths)));
            }

            RefreshList();            
        }
        private bool ExistOnList(string song)
        {
            bool exists = false;
            foreach (var item in _songs)
            {
                if(item.Name == song)
                {
                    exists = true;
                }
            }
            return exists;
        }
        private string GetPath(string fileName, List<string> songsPath = null)
        {
            string actualPath = "";
            if (songsPath == null)
            {
                foreach (var song in _songs)
                {
                    if (song.Name == fileName)
                    {
                        actualPath = song.Path;
                    }
                }
            }
            else
            {
                foreach (var path in songsPath)
                {
                    if(path.Contains(fileName))
                        actualPath = path;
                }
            }

            return actualPath;
        }

        private void songsList_DoubleClick(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.URL = GetPath( songsList.Text );
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Song songToRemove = null;

            foreach (var song in _songs)
            {
                if(song.Name == songsList.Text)
                    songToRemove = song;
            }
            if (songToRemove != null)
                _songs.Remove(songToRemove);

            RefreshList();
        }
        private void RefreshList()
        {
            List<string> songNames = new List<string>();
            foreach (var item in _songs)
                songNames.Add(item.Name);

            songsList.DataSource = null;
            songsList.DataSource = songNames;
        }
    }
}
