using System;

namespace IteratorDesingPattern
{
    internal class Program
    {
        static void Main()
        {
            Canta paraDoluCanta = new Canta();
            var zengin = new Yazilimci(paraDoluCanta) { Isim = "Tosun", Yas = 21 };
            // Zengin birisi olarak, farkli ozelliklerimi farkli siniflar ile temsil ediyorum.
            // Mesela para sayma ozelligim: ParaSayar, saldiri ozelligim: HizliVeOfkeli, vs.
            // GetIterator() genelde standard bir isim. Onun icin kullandim. Geriye Iterator ustipinden ParaSayar tipini gonderiyor.
            ParaSaymaIterator paraSayanOzelligim = zengin.GetIterator();

            double toplamPara = 0;
            // paraSayanOzellik ayni zamanda Cursor olarak calisiyor. Yani bir sonraki parayi getirirken, ayni zamanda hangi parada
            // kaldigimi da biliyor.
            while (paraSayanOzelligim.MoveNext())
            {
                Para para = paraSayanOzelligim.GetCurrent();
                toplamPara += para.Miktar;
                // 500 Dollars bir Dogan SLX
                if (toplamPara >= 500)
                {
                    break;
                }
            }

            TofasGalerisi galery = new TofasGalerisi();
            Tofas doganSLX = galery.VerParayiAlKarayi(toplamPara);
            doganSLX.ArabayiKullan(2, "kilometre");
            // Arkadaslarin tesellisi: 500 dolara tofas almissin, 2 kilometre iyi bile gitmis
            doganSLX.BakimaGotur();
        }
    }

    class Yazilimci
    {
        public string Isim { get; set; }
        public int Yas { get; set; }
        private Canta _canta;

        public Yazilimci(Canta paraDoluCanta)
        {
            _canta = paraDoluCanta;
        }

        public ParaSaymaIterator GetIterator()
        {
            var ozelligim = new ParaSaymaIterator(_canta);
            return ozelligim;
        }
    }

    class ParaSaymaIterator : IterateEden
    {
        private Para[] _toplamPara;
        public Para Current { get; set; }
        private int _index;

        public bool MoveNext()
        {
            if (_index >= _toplamPara.Length)
            {
                return false;
                // Iflas ettim lan.
            }
            Current = _toplamPara[_index++];
            return true;
        }

        public ParaSayma(Canta birCataPara)
        {
            _toplamPara = birCantaPara.ParalariVer();
        }
    }

    class Canta : IterateEdilen
    {
        private Para[] _paralar;

        public Canta()
        {
            _paralar = _biryerlerdenParaGetir();
        }

        private Para[] _biryerlerdenParaGetir()
        {
            throw new NotImplementedException();
        }

        public Para[] TumParalariVer()
        {
            return _paralar;
        }
    }

    class Para
    {
        public string Birim => "USD";
        public double Miktar => 100;
    }
}

