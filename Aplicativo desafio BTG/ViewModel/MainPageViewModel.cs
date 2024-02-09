using AplicativoDesafioBTG.Drawables;
using AplicativoDesafioBTG.Model;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace AplicativoDesafioBTG.ViewModel
{
    public class MainPageViewModel : BaseViewModel
    {
        private string textPrecoInicial;
        public string TextPrecoInicial
        {
            get
            {
                return textPrecoInicial;
            }
            set
            {
                string valorFormatado = !string.IsNullOrEmpty(value) ? Regex.Replace(value, "[^0-9]", string.Empty) : "";
                textPrecoInicial = string.Format("{0:#,##0.00}", Double.Parse(valorFormatado) / 100);
                OnPropertyChanged(nameof(TextPrecoInicial));
            }
        }
        private string textVolatilidadeMedia;
        public string TextVolatilidadeMedia
        {
            get
            {
                return textVolatilidadeMedia;
            }
            set
            {
                textVolatilidadeMedia = value;
                OnPropertyChanged(nameof(TextVolatilidadeMedia));
            }
        }
        private string textRetornoMedio;
        public string TextRetornoMedio
        {
            get
            {
                return textRetornoMedio;
            }
            set
            {
                textRetornoMedio = value;
                OnPropertyChanged(nameof(TextRetornoMedio));
            }
        }
        private string textTempo;
        public string TextTempo
        {
            get
            {
                return textTempo;
            }
            set
            {
                textTempo = value;
                OnPropertyChanged(nameof(TextTempo));
            }
        }
        private IDrawable graficoLinha;
        public IDrawable GraficoLinha
        {
            get { return graficoLinha; }
            set
            {
                graficoLinha = value;
                OnPropertyChanged("GraficoLinha");
            }
        }
        public ICommand GerarSimulacao { get; set; }
        public MainPageViewModel()
        {
            GerarSimulacao = new Command(() => { FuncGeraSimulacao(); });
            GraficoLinha = new LineDrawable();
        }

        private async void FuncGeraSimulacao()
        {
            var valido = await validaEntradaAsync();
            if (valido)
            {
                var preco = Convert.ToDouble(textPrecoInicial);
                var volatilidade = Convert.ToDouble(textVolatilidadeMedia) / 100;
                var retornoMedio = Convert.ToDouble(textRetornoMedio) / 100;
                var dias = Convert.ToInt32(textTempo);
                GraficoLinha = new LineDrawable(
                    new ParametrosSimulacao()
                    {
                        PrecoInicial = preco,
                        TempoDias = dias,
                        VolatilidadeMedia = volatilidade,
                        RetornoMedio = retornoMedio
                    });
            }
        }

        private async Task<bool> validaEntradaAsync()
        {
            Regex regexPorcentagem = new Regex(@"^(\d+|\d+[.]\d+)%?$");
            Regex regexNumerosInteiros = new Regex("^([0-9]+)$");
            if (string.IsNullOrEmpty(textVolatilidadeMedia) || !regexPorcentagem.IsMatch(textVolatilidadeMedia))
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "A porcentagem de volatilidade média está inválida", "OK");
                return false;
            }
            else if (string.IsNullOrEmpty(TextRetornoMedio) || !regexPorcentagem.IsMatch(textRetornoMedio))
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "A porcentagem do retorno médio está inválida", "OK");
                return false;
            }
            else if (string.IsNullOrEmpty(textTempo) || !regexNumerosInteiros.IsMatch(textTempo))
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "O número de dias está inválido", "OK");
                return false;
            }
            else if (string.IsNullOrEmpty(textPrecoInicial))
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "O valor inicial está inválido ou incorreto", "OK");
                return false;
            }
            return true;
        }
    }
}
