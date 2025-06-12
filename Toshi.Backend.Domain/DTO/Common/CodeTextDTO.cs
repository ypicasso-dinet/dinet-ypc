namespace Toshi.Backend.Domain.DTO.Common
{
    public class CodeTextDTO
    {
        public string? code { get; set; }
        public string? text { get; set; }
        public List<CodeTextDTO>? childs { get; set; }

        public CodeTextDTO()
        {
        }

        public CodeTextDTO(string? code, string? text)
        {
            this.code = code;
            this.text = text;
        }

        public CodeTextDTO(string? value)
        {
            this.code = value;
            this.text = value;
        }

        public CodeTextDTO(int value)
        {
            this.code = value.ToString();
            this.text = value.ToString();
        }

        public CodeTextDTO(int? code, string? text)
        {
            this.code = code == null ? "" : code!.ToString();
            this.text = text;
        }
    }
}
