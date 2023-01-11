namespace PizzaPlanet.API.Commons;

public class PropertyRegex
{
    public const string CrustType = "^[A-z_]*$";
    public const string name = "^[A-z_]*$";
    public const string Size = "^[.A-z]";
    public const string StrongPassword = "^(?=.*[A-Z].*[A-Z])(?=.*[!@#$&*])(?=.*[0-9].*[0-9])(?=.*[a-z].*[a-z].*[a-z]).{8}$";
    
    
}