namespace agalloT3.Views;

public partial class vUno : ContentPage
{
    public vUno()
    {
        InitializeComponent();
        pkIdentificacion.SelectedIndexChanged += OnIdentificationTypeChanged;
        dpkFecha.MinimumDate = new DateTime(1900, 1, 1);
        dpkFecha.MaximumDate = DateTime.Today;
    }

    private void OnIdentificationTypeChanged(object sender, EventArgs e)
    {
        txtIdentificacion.Text = string.Empty;

        switch (pkIdentificacion.SelectedIndex)
        {
            case 0:
                txtIdentificacion.Placeholder = "Ingrese 10 d�gitos";
                txtIdentificacion.MaxLength = 10;
                txtIdentificacion.Keyboard = Keyboard.Numeric;
                break;
            case 1:
                txtIdentificacion.Placeholder = "Ingrese 13 d�gitos";
                txtIdentificacion.MaxLength = 13;
                txtIdentificacion.Keyboard = Keyboard.Numeric;
                break;
            case 2:
                txtIdentificacion.Placeholder = "Ingrese n�mero de pasaporte";
                txtIdentificacion.MaxLength = 10;
                txtIdentificacion.Keyboard = Keyboard.Default;
                break;
        }
    }

    private async void btnGuardar_Clicked(object sender, EventArgs e)
    {
        if (!ValidateForm())
            return;

        string tipoIdentificacion = pkIdentificacion.SelectedItem?.ToString() ?? "No seleccionado";
        string numeroIdentificacion = txtIdentificacion.Text.Trim();
        string nombres = txtNombres.Text.Trim();
        string apellidos = txtApellidos.Text.Trim();
        DateTime fecha = dpkFecha.Date;
        string correo = txtCorreo.Text.Trim();
        decimal salario = decimal.Parse(txtSalario.Text);

        await Navigation.PushAsync(new vDos(
            tipoIdentificacion,
            numeroIdentificacion,
            nombres,
            apellidos,
            fecha,
            correo,
            salario
        ));
    }

    private bool ValidateForm()
    {
        if (pkIdentificacion.SelectedIndex == -1)
        {
            DisplayAlert("Error", "Seleccione un tipo de identificaci�n", "OK");
            return false;
        }

        string numeroIdentificacion = txtIdentificacion.Text.Trim();
        if (string.IsNullOrWhiteSpace(numeroIdentificacion))
        {
            DisplayAlert("Error", "Ingrese el n�mero de identificaci�n", "OK");
            return false;
        }

        switch (pkIdentificacion.SelectedIndex)
        {
            case 0:
                if (numeroIdentificacion.Length != 10 || !numeroIdentificacion.All(char.IsDigit))
                {
                    DisplayAlert("Error", "La c�dula debe tener exactamente 10 d�gitos", "OK");
                    return false;
                }
                break;
            case 1:
                if (numeroIdentificacion.Length != 13 || !numeroIdentificacion.All(char.IsDigit))
                {
                    DisplayAlert("Error", "El RUC debe tener exactamente 13 d�gitos", "OK");
                    return false;
                }
                break;
            case 2:
                if (string.IsNullOrWhiteSpace(numeroIdentificacion) || numeroIdentificacion.Length > 10)
                {
                    DisplayAlert("Error", "El pasaporte no debe exceder 10 caracteres", "OK");
                    return false;
                }
                break;
        }

        if (string.IsNullOrWhiteSpace(txtNombres.Text) || !txtNombres.Text.Trim().All(c => char.IsLetter(c) || c == ' '))
        {
            DisplayAlert("Error", "Ingrese nombres v�lidos (solo letras y espacios)", "OK");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtApellidos.Text) || !txtApellidos.Text.Trim().All(c => char.IsLetter(c) || c == ' '))
        {
            DisplayAlert("Error", "Ingrese apellidos v�lidos (solo letras y espacios)", "OK");
            return false;
        }

        if (dpkFecha.Date == default || dpkFecha.Date > DateTime.Today)
        {
            DisplayAlert("Error", "Seleccione una fecha v�lida", "OK");
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtCorreo.Text) || !IsValidEmail(txtCorreo.Text))
        {
            DisplayAlert("Error", "Ingrese un correo electr�nico v�lido", "OK");
            return false;
        }

        if (!decimal.TryParse(txtSalario.Text, out decimal salario) || salario <= 0)
        {
            DisplayAlert("Error", "Ingrese un salario v�lido (mayor a 0)", "OK");
            return false;
        }

        return true;
    }

    private bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }
}