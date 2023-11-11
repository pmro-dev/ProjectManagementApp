namespace Web.Common;

public abstract class BasicModelAbstract
{
	public virtual int Id { get; set; }
	public virtual string Title { get; set; } = string.Empty;
}
