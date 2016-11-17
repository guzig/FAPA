using System;

namespace FaPA.Infrastructure.Dto
{
    public abstract class NotDaoBaseEntityDto : BaseEntityDto
    {
        protected NotDaoBaseEntityDto()
        {
            Id = Guid.NewGuid().GetHashCode();
            //OnDataErrorInfo( OnDataErrorInfo );
        }

        protected override bool IsTransient()
        {
            return false;
        }


        //public override string Error => null;

        //private void OnDataErrorInfo( object sender, ValidationEventArgs arg )
        //{

        //}

        //public override string this[string columnName]
        //{
        //    get
        //    {
        //        return CoreValidatorService.GetValidationErrors( columnName, this )?.FirstOrDefault();

        //    }
        //}

        //public override string this[string columnName]
        //{
        //    get
        //    {
        //        //var arg = new ValidationEventArgs( columnName );
        //        //OnDataErrorInfoChanged( arg );
        //        //return arg.Result;
        //        return null;
        //    }
        //}

        //public NotDaoBaseEntityDto ShallowCopy()
        //{
        //    return ( NotDaoBaseEntityDto ) this.MemberwiseClone();
        //}

    }
}