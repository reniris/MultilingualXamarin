using MultilingualXamarin.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace MultilingualXamarin.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void RaisePropertyChanged([CallerMemberName]string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        /// <summary>
        /// 選択されているカルチャ
        /// </summary>
        #region SelectedCulture変更通知プロパティ
        private CultureInfo _SelectedCulture = CultureInfo.GetCultureInfo(ResourceService.Current.SupportedCultures.First().Name);

        public CultureInfo SelectedCulture
        {
            get
            { return _SelectedCulture; }
            set
            {
                if (_SelectedCulture == value)
                    return;
                _SelectedCulture = value;
                RaisePropertyChanged(nameof(SelectedCulture));

                ChangeCulture(value);
            }
        }
        #endregion


        /// <summary>
        /// サポートしているカルチャのリスト
        /// </summary>
        public IReadOnlyCollection<CultureInfo> Cultures { get { return ResourceService.Current.SupportedCultures; } }

        /// <summary>
        /// カルチャ変更
        /// </summary>
        /// <param name="x">x</param>
        private void ChangeCulture(CultureInfo x)
        {
            ResourceService.Current.ChangeCulture(x.Name);
        }
    }
}
