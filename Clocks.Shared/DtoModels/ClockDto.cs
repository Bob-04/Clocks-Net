using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Clocks.Shared.DtoModels
{
    public class ClockDto : INotifyPropertyChanged
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string TimeZoneId { get; set; }

        private string _currentTime { get; set; }

        public string CurrentTime
        {
            get => _currentTime;
            set
            {
                _currentTime = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
