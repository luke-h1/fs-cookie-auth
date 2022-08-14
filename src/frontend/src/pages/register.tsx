import { useRouter } from 'next/router';
import { FormEvent, useState } from 'react';
import Page from '../components/Page';
import authApi from '../utils/authApi';

const RegisterPage = () => {
  const router = useRouter();
  const [name, setName] = useState<string>('');
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [submitting, setSubmitting] = useState<boolean>(false);

  const submitForm = async (e: FormEvent<HTMLFormElement>) => {
    setSubmitting(true);
    e.preventDefault();

    await authApi.post('/api/auth/register', {
      name,
      email,
      password,
    });

    router.push('/login');
  };

  return (
    <Page>
      <form onSubmit={submitForm}>
        <h1>Please register</h1>
        <div>
          <input
            type="name"
            placeholder="Name"
            value={name}
            onChange={e => setName(e.target.value)}
            required
          />

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
            Register
          </button>
        </div>
      </form>
    </Page>
  );
};

export default RegisterPage;
