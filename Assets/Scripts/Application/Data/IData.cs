using System;

public interface IData : ICloneable
{
    public event Action<IData> OnDataRemove;

    public int Id { get; set; }
    public int InstanceId { get; set; }

    public  IDataTable TableModel { get; set; }
}
