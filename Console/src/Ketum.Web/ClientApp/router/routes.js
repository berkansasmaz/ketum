import MonitoringList from 'components/pages/monitoring/list';
import MonitoringSave from 'components/pages/monitoring/save';
import Forbidden from 'components/root/forbidden';

export const routes = [{
    name: 'monitoring-list',
    path: '/monitoring/list',
    component: MonitoringList,
    display: 'Monitoring',
    icon: 'chart-line'
  },
  {
    divider: true,
    path: ''
  },
  {
    name: 'monitoring-save',
    path: '/monitoring/save',
    component: MonitoringSave,
    display: 'New Monitoring',
    icon: 'plus'
  },
  {
    divider: true,
    path: ''
  },
  {
    name: 'account-view',
    path: '/Identity/Account/Manage',
    display: 'Account',
    icon: 'user-circle'
  },
  {
    name: 'account-subscription',
    path: '/Subscription',
    display: 'Subscription',
    icon: 'credit-card'
  },
  {
    name: 'forbidden',
    path: '/forbidden',
    hidden: true,
    component: Forbidden
  }
];
