using MultilingualXamarin.Properties;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace MultilingualXamarin.Model
{
    /// <summary>
    /// 多言語化されたリソースと、言語の切り替え機能を提供します。
    /// </summary>
    public class ResourceService : INotifyPropertyChanged
    {
        #region singleton members

        public static ResourceService Current { get; } = new ResourceService();

        #endregion

        public string this[string key] => Resources.ResourceManager.GetString(key, Resources.Culture);

        #region INotifyPropertyChanged members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        /// <summary>
        /// 指定されたカルチャ名を使用して、リソースのカルチャを変更します。
        /// </summary>
        /// <param name="name">カルチャの名前。</param>
        public void ChangeCulture(string name)
        {
            Resources.Culture = CultureInfo.GetCultureInfo(name);
            this.RaisePropertyChanged("Item");  //気持ち悪いけどこうするしかないんだ
        }

        /// <summary>
		/// サポートされているカルチャの名前。
		/// </summary>
		private readonly string[] supportedCultureNames =
        {
            "ja", // Resources.resx
			"en",
            "de",
        };

        /// <summary>
        /// サポートされているカルチャを取得します。
        /// </summary>
        public IReadOnlyCollection<CultureInfo> SupportedCultures { get; }

        private ResourceService()
        {
            this.SupportedCultures = this.supportedCultureNames
                .Select(x =>
                {
                    try
                    {
                        return CultureInfo.GetCultureInfo(x);
                    }
                    catch (CultureNotFoundException)
                    {
                        return null;
                    }
                })
                .Where(x => x != null)
                .ToList();
        }
    }
}
