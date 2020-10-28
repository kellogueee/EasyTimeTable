using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace EasyTimeTable.ViewModels
{
    public class FlagSettingsViewModel:BaseViewModel
    {
        bool isSkipCarousel;

        public FlagSettingsViewModel(IDictionary<string,object>dictionary)
        {
            IsSkipCarousel = GetDictionaryEntry<bool>(dictionary, "IsSkipCarousel");
        }

        public bool IsSkipCarousel
        {
            set { SetProperty(ref isSkipCarousel, value); }
            get { return isSkipCarousel; }
        }

        public void SaveState(IDictionary<string,object> dictionary)
        {
            dictionary["IsSkipCarousel"] = IsSkipCarousel;
        }

        T GetDictionaryEntry<T>(IDictionary<string,object> dictionary, string key, T defaultValue = default(T))
        {
            return dictionary.ContainsKey(key) ? (T)dictionary[key] : defaultValue;
        }

        bool SetProperty<T>(ref T storage, T value, [CallerMemberName]string properyName = null)
        {
            if (object.Equals(storage, value))
            {
                return false;
            }
            storage = value;
            OnPropertyChanged(properyName);
            return true;
        }

    }
}
