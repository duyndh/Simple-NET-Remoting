using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotableObjects
{
	public enum EAction
	{
		Init,
		Connected,
		Release
	};

	public interface IObserver
	{
		string Notify(EAction action, string text);
	}
}
