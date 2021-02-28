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

        public DateTime CurrentTime { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
