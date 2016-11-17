namespace FaPA.GUI.Feautures.Anagrafica
{
    public class AnagraficaEntityTypeConverter : AutoMapper.TypeConverter<Core.Anagrafica, Core.Anagrafica>
    {
        protected override Core.Anagrafica ConvertCore(Core.Anagrafica source)
        {
            //switch (source.EntityType)
            //{
            //    case EntityType.One:
            //        return AutoMapper.Mapper.Map<EntityOne>(source);
            //    case EntityType.Two:
            //        return AutoMapper.Mapper.Map<EntityTwo>(source);
            //    default:
            //        return AutoMapper.Mapper.Map<EntityThree>(source);
            //}

            return source;
        }
    }
}