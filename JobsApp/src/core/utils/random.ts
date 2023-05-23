export const getRandomRgb = () => {
  const random = Math.round(0xffffff * Math.random());

  return `#${random.toString(16)}`;
};
