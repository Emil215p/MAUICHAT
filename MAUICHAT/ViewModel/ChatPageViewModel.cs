using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUICHAT;

public class ChatPageViewModel : INotifyPropertyChanged
{
    #region
    public MessageViewModel messageViewModel { get; } = new MessageViewModel();
    #endregion

    #region editor
    private string? _editorContent;
    public string? EditorContent
    {
        get { return _editorContent; }
        set
        {
            if (_editorContent != value)
            {
                _editorContent = value;
                OnPropertyChanged(nameof(EditorContent));
            }
        }
    }
    #endregion

    #region nameContent
    private string? _nameContent;
    public string? NameContent
    {
        get { return _nameContent; }
        set
        {
            if (_nameContent != value)
            {
                _nameContent = value;
                OnPropertyChanged(nameof(NameContent));
            }
        }
    }
    #endregion

    #region PropertyChanged
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    #endregion
}
