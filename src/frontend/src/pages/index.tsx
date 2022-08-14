import { useEffect, useState } from 'react';
import Page from 'src/components/Page';
import authApi from 'src/utils/authApi';

const Home = () => {
  const [message, setMessage] = useState<string>('');
  const [auth, setAuth] = useState<boolean>(false);

  const getAuth = async () => {
    try {
      const res = await authApi.get('/api/auth/user');
      setMessage(`Hello ${res.data.name} 👋`);
      setAuth(true);
    } catch (e) {
      setMessage("You're not logged in");
      setAuth(false);
    }
  };

  useEffect(() => {
    getAuth();
  }, []);

  return (
    <Page auth={auth}>
      <h2>{message}</h2>
    </Page>
  );
};

export default Home;
