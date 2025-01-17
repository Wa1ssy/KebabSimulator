namespace Kebab_Simulator.Models.Profiles
{
    public class ProfileRegisterViewModel
    {
        public Guid ID { get; set; }
        public string ApplicationUserID { get; set; }
        public string ScreenName { get; set; }
        public int KebabCredits { get; set; }
        public int ScrapResource { get; set; }
        public List<KebabOwnership> MyKebabs { get; set; }
        public int Victories { get; set; }
        public int MyProperty { get; set; }
        public string? MySolarSystem { get; set; }
        public ProfileStatus CurrentStatus { get; set; }
        public bool ProfileType { get; set; }
    }
}
