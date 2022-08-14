import Link from 'next/link';
import { useRouter } from 'next/router';
import { ReactNode } from 'react';
import authApi from '../utils/authApi';

interface Props {
  auth?: boolean;
  children: ReactNode;
}

const Page = ({ auth, children }: Props) => {
  const router = useRouter();

  const logout = async () => {
    await authApi.post('/api/auth/logout');
    router.push('/login');
  };

  let menu;

  if (!auth) {
    menu = (
      <ul>
        <li>
          <Link href="/login">Login</Link>
        </li>
        <li>
          <Link href="/register">Register</Link>
        </li>
      </ul>
    );
  } else {
    menu = (
      <ul>
        <li>
          <button onClick={logout} type="button">
            Logout
          </button>
        </li>
      </ul>
    );
  }

  return (
    <div>
      <header>Auth test next.js x dotnet</header>
      <div style={{ marginBottom: '5rem' }}>{menu}</div>
      <main>{children}</main>
    </div>
  );
};

export default Page;
