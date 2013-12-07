using System;
using System.Collections.Generic;
using System.Threading;
using Cirrious.MvvmCross.ViewModels;

namespace Thing.Core.ViewModels
{
    public class FirstViewModel 
		: MvxViewModel
    {
        private int _numNavs;
        public int NumNavs 
        {   
            get { return _numNavs; }
            set { _numNavs = value; RaisePropertyChanged(() => NumNavs); }
        }

        private Timer _timer;

        public void OnShow()
        {
            ClearTimer();
            _timer = new Timer(ignored =>
                {
                    ClearTimer();
                    NumNavs++;
                    ShowViewModel<SecondViewModel>();
                },
            null, 5000, Timeout.Infinite);
        }

        private void ClearTimer()
        {
            if (_timer != null)
            {
                _timer.Dispose();
                _timer = null;
            }
        }
    }

    public interface IInnerService
    {
        List<InnerService.It> Items { get; set; }
    }

    public class InnerService
        : MvxNotifyPropertyChanged, IInnerService
    {
        public class It
            : MvxNotifyPropertyChanged
        {
            private bool _flag;
            public bool Flag 
            {   
                get { return _flag; }
                set { _flag = value; RaisePropertyChanged(() => Flag); }
            }

            private int _itemNumber;
            public int ItemNumber 
            {   
                get { return _itemNumber; }
                set { _itemNumber = value; RaisePropertyChanged(() => ItemNumber); }
            }

            public It(int i)
            {
                _itemNumber = i;
                _flag = i%2 == 0;
            }
        }

        private Timer _timer;

        public InnerService()
        {
            Items = new List<It>();
            for (var i = 0; i < 100; i++)
            {
                Items.Add(new It(i));
            }
            _timer = new Timer(ignored =>
                {
                    foreach (var item in Items)
                    {
                        item.Flag = !item.Flag;
                    }
                }, null, 1000, 1000);
        }

        private List<It> _items;
        public List<It> Items 
        {   
            get { return _items; }
            set { _items = value; RaisePropertyChanged(() => Items); }
        }
    }

    public class SecondViewModel
        : MvxViewModel
    {
        private IInnerService _innerService;
        public IInnerService InnerService 
        {   
            get { return _innerService; }
            set { _innerService = value; RaisePropertyChanged(() => InnerService); }
        }

        private Timer _timer;

        public SecondViewModel(IInnerService innerService)
        {
            _innerService = innerService;
            _timer = new Timer(ignored =>
                {
                    _timer.Dispose();
                    _timer = null;
                    Close(this);
                },
                null, 20000, Timeout.Infinite);
        }
    }
}
