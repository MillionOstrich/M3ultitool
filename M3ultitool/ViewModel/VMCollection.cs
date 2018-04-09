using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace M3ultitool.ViewModel
{
    public class VMCollection<T, TViewModel> : ObservableCollection<TViewModel> 
        where TViewModel : IViewModel<T>
    {
        private bool _updating;
        private ICollection<T> _innerCollection;
        public ICollection<T> InnerCollection { get { return _innerCollection; } }

        //because we are calling it in the constructor
        public override sealed event NotifyCollectionChangedEventHandler CollectionChanged
        {
            add { base.CollectionChanged += value; }
            remove { base.CollectionChanged -= value; }
        }

        public VMCollection(ICollection<T> collection) : base()
        {
            _updating = false;
            _innerCollection = collection ?? throw new ArgumentNullException();
            CollectionChanged += HandleCollectionChanged;
        }

        private void HandleCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(_updating) { return; }
            _updating = true;
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (TViewModel x in e.NewItems) { _innerCollection.Add(x.Model); }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach(TViewModel x in e.OldItems) { _innerCollection.Remove(x.Model); }
                    break;
                case NotifyCollectionChangedAction.Move:
                    ResyncInnerCollection();
                    break;
                case NotifyCollectionChangedAction.Replace:
                    ResyncInnerCollection();
                    break;
                case NotifyCollectionChangedAction.Reset:
                    _innerCollection.Clear();
                    break;
            }
            _updating = false;
        }

        public void Refresh(Func<T, TViewModel> construct)
        {
            if(_updating) { return; }
            _updating = true;
            Clear();
            foreach(T x in _innerCollection) { Add(construct(x)); }
            _updating = false;
        }

        private void ResyncInnerCollection()
        {
            _innerCollection.Clear();
            foreach(TViewModel x in this) { _innerCollection.Add(x.Model); }
        }
    }
}
