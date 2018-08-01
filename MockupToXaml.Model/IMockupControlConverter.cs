namespace MockupToXaml.Model
{
    public interface IMockupControlConverter
    {
        MockupTemplate Template { get; set; }

        string ConvertMockupToXaml(MockupControl control);
    }
}