﻿namespace po;

public partial class MainPage : ContentPage
{
	const int gravidade = 1;
	const int TempoEntreFrames = 25;
	bool estaMorto = true;
	double larguraJanela = 0;
	double alturaJanela = 0;
	int velocidade = 20;
	public MainPage()
	{
		InitializeComponent();
	}

	void AplicaGravidade()
	{
		cuphed.TranslationY += gravidade;

	}
	protected override void OnAppearing()
	{
		base.OnAppearing();
		Desenha();
	}
	public async void Desenha()
	{
		while (!estaMorto)
		{
			AplicaGravidade();
			await Task.Delay(TempoEntreFrames);
			GerenciarCanos();
		}
	}

	void La(Object s, TappedEventArgs e)
	{
		GameOverFrame.IsVisible = false;
		estaMorto = false;
		Inicializar();
		Desenha();
	}
	void Inicializar()
	{
		cuphed.TranslationY = 0;
	}

	protected override void OnSizeAllocated(double w, double h)
	{
		base.OnSizeAllocated(w, h);
		larguraJanela = w;
		alturaJanela = h;
	}

	void GerenciarCanos()
	{
		FlorAlto.TranslationX -= velocidade;
		FlorBaixo.TranslationX -= velocidade;
		if(FlorBaixo.TranslationX < -larguraJanela){
			FlorBaixo.TranslationX = 20;
			FlorAlto.TranslationX = 20;
		}
	}
}



