using System.Collections;

namespace DataTablesServerSide.Helpers
{
    public class DtResponse
    {
        public int Draw { get { return _draw; } }
        public int RecordsFiltered { get { return _recordsFiltered; } }
        public int RecordsTotal { get { return _recordsTotal; } }
        public IEnumerable Data { get { return _data; } }

        private int _draw { get; set; }
        private int _recordsFiltered { get; set; }
        private int _recordsTotal { get; set; }
        private IEnumerable _data { get; set; }

        public DtResponse(int _draw, int _recordsFiltered, int _recordsTotal, IEnumerable _data)
        {
            this._draw = _draw;
            this._recordsFiltered = _recordsFiltered;
            this._recordsTotal = _recordsTotal;
            this._data = _data;
        }

        public static DtResponse CreateResponse(int draw, int recordsFiltered, int recordsTotal, IEnumerable data)
        {
            return new DtResponse(draw, recordsFiltered, recordsTotal, data); ;
        }
    }
}
