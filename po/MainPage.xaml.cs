namespace po;

public partial class MainPage : ContentPage
{
	const int gravidade = 5;
	const int TempoEntreFrames = 25;
	const int forcaPulo = 30;
	const int maxTempoPulando = 3;
	const int aberturaMinima = 100;

	bool estaMorto = true;
	bool estaPulando = false;

	int velocidade = 5;
	int tempoPulando = 0;
	int score = 0;

	double LarguraJanela;
	double AlturaJanela;

	public MainPage()
	{
		InitializeComponent();
	}

	void AplicaGravidade()
	{
		cuphed.TranslationY += gravidade;

	}

	public async void Desenha()
	{
		while (!estaMorto)
		{
			GerenciarCanos();
			if (estaPulando)
				AplicaPulo();
			else
				AplicaGravidade();
			if (VerificaColisao())
			{
				estaMorto = true;
				GameOverFrame.IsVisible = true;
				break;
			}
			await Task.Delay(TempoEntreFrames);
		}
	}


	void Inicializar()
	{
		cuphed.TranslationY = 0;
		estaMorto = false;
		FlorAlto.TranslationX = 0;
		FlorBaixo.TranslationX = 0;

		FlorAlto.TranslationX = -LarguraJanela;
		FlorBaixo.TranslationX = -LarguraJanela;
		cuphed.TranslationX = 0;
		cuphed.TranslationY = 0;
		velocidade = 5;
		score = 0;
		GerenciarCanos();
	}

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		LarguraJanela = w;
		AlturaJanela = h;
	}

	void GerenciarCanos()
	{
		FlorAlto.TranslationX -= velocidade;
		FlorBaixo.TranslationX -= velocidade;

		if (FlorBaixo.TranslationX < -LarguraJanela)
		{
			FlorBaixo.TranslationX = 40;
			FlorAlto.TranslationX = 40;

			var alturaMaxima = -100;
			var alturaMinima = -FlorBaixo.HeightRequest;

			FlorAlto.TranslationY = Random.Shared.Next((int)alturaMinima, (int)alturaMaxima);
			FlorBaixo.TranslationY = FlorAlto.TranslationY + aberturaMinima + FlorBaixo.HeightRequest;

			LabelScore.Text = "Canos:" + score.ToString("D3");
		}
		score++;
		if (score % 4 == 0)
			velocidade++;
	}

	void OnGameOverClicked(object s, TappedEventArgs e)
	{
		GameOverFrame.IsVisible = false;
		Inicializar();
		Desenha();
	}

	bool VerificaColisaoTeto()
	{
		var minY = -AlturaJanela / 2;
		if (cuphed.TranslationY <= minY)
			return true;
		else
			return false;
	}

	bool VerificaColisaoChao()
	{
		var maxY = AlturaJanela / 2;
		if (cuphed.TranslationY >= maxY)
			return true;
		else
			return false;
	}

	bool VerificaColisao()
	{
		return VerificaColisaoTeto() ||
		   	   VerificaColisaoChao() ||
		   	   VerificaColisaoCanoCima() ||
		   	   VerificaColisaoCanoBaixo();
	}

	void AplicaPulo()
	{
		cuphed.TranslationY -= forcaPulo;
		tempoPulando++;

		if (tempoPulando >= maxTempoPulando)
		{
			estaPulando = false;
			tempoPulando = 0;
		}
	}

	void OnGridClicked(object s, TappedEventArgs args)
	{
		estaPulando = true;
	}

	bool VerificaColisaoCanoCima()
	{
		var minY = AlturaJanela / 2;
		var posHPardal = (LarguraJanela / 2) - (cuphed.WidthRequest / 2);
		var posVPardal = (AlturaJanela / 2) - (cuphed.HeightRequest / 2) + cuphed.TranslationY;
		if (posHPardal >= Math.Abs(FlorAlto.TranslationX) - FlorAlto.WidthRequest &&
		 posHPardal >= Math.Abs(FlorAlto.TranslationX) - FlorAlto.WidthRequest &&
		 posHPardal <= FlorAlto.HeightRequest + cuphed.TranslationY)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	bool VerificaColisaoCanoBaixo()
	{
		var posicaoHorizontalPardal = LarguraJanela - 50 - cuphed.WidthRequest / 2;
		var posicaoVerticalPardal = (AlturaJanela / 2) + (cuphed.HeightRequest / 2) + cuphed.TranslationY;

		var yMaxCano = FlorAlto.HeightRequest + FlorAlto.TranslationY + aberturaMinima;

		if (
			 posicaoHorizontalPardal >= Math.Abs(FlorAlto.TranslationX) - FlorAlto.WidthRequest &&
			 posicaoHorizontalPardal <= Math.Abs(FlorAlto.TranslationX) + FlorAlto.WidthRequest &&
			 posicaoVerticalPardal >= yMaxCano
		   )
			return true;
		else
			return false;
	}
}