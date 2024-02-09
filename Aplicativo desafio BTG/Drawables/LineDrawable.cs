using AplicativoDesafioBTG.Model;

namespace AplicativoDesafioBTG.Drawables
{
    public class LineDrawable : IDrawable
    {
        public ParametrosSimulacao? parametros;
        public LineDrawable()
        {
        }
        public LineDrawable(ParametrosSimulacao parametrosSimulacao)
        {
            parametros = parametrosSimulacao;
        }

        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
            SolidPaint solidPaint = new SolidPaint(Color.FromArgb("#1d1d3d"));
            RectF solidRectangle = new RectF(0, 0, App.larguraGrafico + 50, App.alturaGrafico + 50);
            canvas.SetFillPaint(solidPaint, solidRectangle);
            canvas.FillRectangle(solidRectangle);
            canvas.StrokeColor = Colors.LightBlue;
            canvas.StrokeSize = 1;
            if (parametros != null)
            {
                var retorno = BrownianMotion.GenerateBrownianMotion(parametros.VolatilidadeMedia, parametros.RetornoMedio, parametros.PrecoInicial, parametros.TempoDias);
                float percentualValores = Convert.ToSingle(100 / retorno.Max());//Percentural de valores do intervalo
                double percentualLarguraTela = (double)Math.Round(App.larguraGrafico / Convert.ToDecimal(parametros.TempoDias), 8);
                double percentualAlturaTela = Math.Round(App.alturaGrafico / 100.0, 8);
                float x1 = 0;
                float x2;
                float y1 = App.alturaGrafico - Convert.ToSingle(percentualAlturaTela * Convert.ToSingle(retorno[0] * percentualValores));
                float y2;

                for (int i = 1; i < retorno.Count(); i++)
                {
                    var proximoValor = App.alturaGrafico - Convert.ToSingle(percentualAlturaTela * (Convert.ToSingle(retorno[i] * percentualValores)));
                    y2 = proximoValor;
                    x2 = (float)(x1 + percentualLarguraTela);
                    canvas.DrawLine(x1, y1, x2, y2);
                    x1 += (float)percentualLarguraTela;
                    y1 = proximoValor;
                }
            }

        }
    }
}
