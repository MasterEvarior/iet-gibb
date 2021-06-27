export function setJWT(jwt) {
  localStorage.setItem('token', jwt);
}

export function unsetJWT() {
  localStorage.removeItem('token');
}

export function getJWT() {
  return localStorage.getItem('token');
}

export function isAuthenticated() {
  return !!getJWT();
}

export function authGuard(to, from, next) {
  if (!isAuthenticated()) next({ name: 'Login' })
  else next()
}