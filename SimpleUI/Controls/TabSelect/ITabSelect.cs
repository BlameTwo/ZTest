using System.Collections.Generic;

namespace SimpleUI.Controls;

public interface ITabSelect
{
    ITabSelectItem SelectItem { get; set; }

    List<ITabSelectItem> Items { get; set; }


}
