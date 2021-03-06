using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Clocks.Shared.DtoModels
{
    public class ClockDto : INotifyPropertyChanged
    {
        public Guid Id { get; set; }

        private string _name { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _timeZoneId { get; set; }

        public string TimeZoneId
        {
            get => _timeZoneId;
            set
            {
                _timeZoneId = value;
                OnPropertyChanged(nameof(TimeZoneId));
            }
        }

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
