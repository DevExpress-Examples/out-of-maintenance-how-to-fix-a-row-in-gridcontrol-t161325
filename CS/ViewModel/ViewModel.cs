using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication41 {
    public class ViewModel {
        public virtual ObservableCollection<Customer> FixedTopRows {
            get;
            set;
        }
        public ObservableCollection<Customer> Customers {
            get;
            set;
        }

        public ViewModel() {
            Customers = new ObservableCollection<Customer>();
            for (int i = 1; i < 30; i++) {
                Customers.Add(new Customer() { ID = i, Name = "Name" + i });
            }

            FixedTopRows = new ObservableCollection<Customer>();
            FixedTopRows.Add(Customers[5]);
            FixedTopRows.Add(Customers[20]);
        }
    }

    public class Customer {
        public int ID {
            get;
            set;
        }

        public string Name {
            get;
            set;
        }
    }
}
