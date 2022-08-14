import { useRouter } from 'next/router';
import { FormEvent, useState } from 'react';
import Page from '../components/Page';
import authApi from '../utils/authApi';

const LoginPage = () => {
  const router = useRouter();
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [submitting, setSubmitting] = useState<boolean>(false);

  const submitForm = async (e: FormEvent<HTMLFormElement>) => {
    setSubmitting(true);
    e.preventDefault();

    await authApi.post('/api/auth/login', {
      email,
      password,
    });

    router.push('/');
  };

  return (
    <Page>
      <form onSubmit={submitForm}>
        <h1>Please sign in</h1>
        <div>
          <input
            type="email"
            placeholder="Email"
            value={email}
            onChange={e => setEmail(e.target.value)}
            required
          />

          <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={e => setPassword(e.target.value)}
            required
          />

          <button type="submit" disabled={submitting}>
            Sign in
          </button>
        </div>
      </form>
    </Page>
  );
};

export default LoginPage;
