namespace agalloT3.Views;

public partial class vDos : ContentPage
{
    public vDos(string tipoIdentificacion, string numeroIdentificacion, string nombres, string apellidos,
        DateTime fecha, string correo, decimal salario)
    {
        InitializeComponent();
        DisplayContactInfo(tipoIdentificacion, numeroIdentificacion, nombres, apellidos, fecha, correo, salario);
    }

    private void DisplayContactInfo(string tipoIdentificacion, string numeroIdentificacion, string nombres,
        string apellidos, DateTime fecha, string correo, decimal salario)
    {
        lblTipoIdentificacion.Text = tipoIdentificacion;
        lblNumeroIdentificacion.Text = numeroIdentificacion;
        lblNombres.Text = nombres;
        lblApellidos.Text = apellidos;
        lblFecha.Text = fecha.ToString("dd/MM/yyyy");
        lblCorreo.Text = correo;
        lblSalario.Text = salario.ToString("C");

        decimal aporteIESS = CalculateIESSContribution(salario);
        lblAporteIESS.Text = aporteIESS.ToString("C");
    }

    private decimal CalculateIESSContribution(decimal salario)
    {
        const decimal iessRate = 0.0945m;
        return salario * iessRate;
    }

    private async void btnExportar_Clicked(object sender, EventArgs e)
    {
        try
        {
            string fileName = $"contacto_{lblNumeroIdentificacion.Text}_{DateTime.Now:yyyyMMddHHmmss}.txt";
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), fileName);

            string contenido = $"Tipo de Identificación: {lblTipoIdentificacion.Text}\n" +
                              $"Número de Identificación: {lblNumeroIdentificacion.Text}\n" +
                              $"Nombres: {lblNombres.Text}\n" +
                              $"Apellidos: {lblApellidos.Text}\n" +
                              $"Fecha de Nacimiento: {lblFecha.Text}\n" +
                              $"Correo Electrónico: {lblCorreo.Text}\n" +
                              $"Salario: {lblSalario.Text}\n" +
                              $"Aporte al IESS: {lblAporteIESS.Text}\n" +
                              $"Fecha de Exportación: {DateTime.Now:dd/MM/yyyy HH:mm:ss}";

            await File.WriteAllTextAsync(filePath, contenido);

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = "Exportar Contacto",
                File = new ShareFile(filePath)
            });

            await DisplayAlert("Éxito", $"Datos exportados correctamente en {filePath}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Error al exportar: {ex.Message}", "OK");
        }
    }
}