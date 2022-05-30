namespace DataTablesServerSide.Helpers
{
    public class DtRequest
    {
        public int Draw { get; set; }
        public int Start { get; set; }
        public int Length { get; set; }
        public List<DtColumn> Columns { get; set; }
        public List<DtOrder> Order { get; set; }
        public DtSearch Search { get; set; }
    }

    public class DtColumn
    {
        public string Data { get; set; }
        public string Name { get; set; }
    }

    public class DtSearch
    {
        public string Value { get; set; }
        public string Regex { get; set; }
    }

    public class DtOrder
    {
        public int Column { get; set; }
        public string Dir { get; set; }
    }
}
