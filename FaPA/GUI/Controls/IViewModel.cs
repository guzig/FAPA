namespace FaPA.GUI.Controls
{
    public interface IViewModel
    {
        void Init();
        void PersitEntity();
        void MakeTransient();
        void AddEntity();
        void CancelEdit();
    }
}