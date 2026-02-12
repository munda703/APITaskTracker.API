import { Link, NavLink } from 'react-router-dom';

export default function Header() {
  return (
    <header className="bg-blue-600 text-white shadow">
      <div className="max-w-7xl mx-auto px-6 py-4 flex justify-between items-center">
        <NavLink
          to="/tasks"
          className="text-lg font-semibold hover:underline"
        >
          Tasks
        </NavLink>

        <Link to="/tasks" className="text-xl font-bold">
          Task Tracker
        </Link>
      </div>
    </header>
  );
}
