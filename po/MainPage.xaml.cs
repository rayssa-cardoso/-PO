namespace po;

public partial class MainPage : ContentPage
{
	 const int gravidade =1;
	 const int TempoEntreFrames=25;
	 bool estaMorto = false;
	public MainPage()
	{
		InitializeComponent();
	}

	 void AplicaGravidade()
    { 
	  cuphead.png.TranslationY+=gravidade;



	}


	}

}

