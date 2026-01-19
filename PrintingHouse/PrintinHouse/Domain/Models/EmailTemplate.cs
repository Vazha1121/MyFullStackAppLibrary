namespace PrintingHouse.Domain.Models
{
    public class EmailTemplate
    {
        public int EmailTemplateID {  get; set; }
        public string EmailTemplateName { get; set; }
        public string EmailTemplateSubject { get; set; }
        public string EmailTemplateBody { get; set; }
    }
}
