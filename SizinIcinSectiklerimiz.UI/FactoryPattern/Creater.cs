namespace SizinIcinSectiklerimiz.UI.FactoryPattern
{
    class Creater
    {
        public FactoryData FactoryMethod(Datas datas)
        {
            FactoryData factoryData = null;
            switch (datas)
            {
                case Datas.Json:
                    factoryData = new JsonData();
                    break;
                case Datas.Xml:
                    factoryData = new XmlData();
                    break;
                default:
                    break;
            }
            return factoryData;
        }
    }
}
