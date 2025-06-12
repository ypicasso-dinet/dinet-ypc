namespace Toshi.Backend.Domain.DTO.IngresoPollo
{
    public class IngresoPolloListResponseDTO
    {
        public List<IngresoPolloTab1DTO>? info_tab1 { get; set; }
        public List<IngresoPolloTab2_1DTO>? info_tab2_1 { get; set; }
        public List<IngresoPolloTab2_2DTO>? info_tab2_2 { get; set; }
        public List<IngresoPolloTab3DTO>? info_tab3 { get; set; }

        public List<string?>? lineas { get; set; }
        public List<string?>? lotes { get; set; }
    }
}
