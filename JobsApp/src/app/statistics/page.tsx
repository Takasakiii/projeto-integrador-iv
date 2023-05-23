"use client";

import { statisticApi } from "@/core/api/statistic";
import { skillLevelText } from "@/core/models/user";
import { getRandomRgb } from "@/core/utils/random";
import { useQuery } from "@tanstack/react-query";
import {
  BarElement,
  CategoryScale,
  Chart,
  Legend,
  LinearScale,
  Title,
  Tooltip,
} from "chart.js";
import { NextPage } from "next";
import { Bar } from "react-chartjs-2";

Chart.register(CategoryScale, LinearScale, BarElement, Title, Tooltip, Legend);

const StatisticsPage: NextPage = () => {
  const queryMostUsedLanguage = useQuery(
    ["statistic", "most-used-language"],
    () => statisticApi.mostUsedLanguage()
  );

  const queryLessUsedLanguage = useQuery(
    ["statistic", "less-used-language"],
    () => statisticApi.lessUsedLanguage()
  );

  const queryAverageValueLanguage = useQuery(
    ["statistic", "average-value-language"],
    () => statisticApi.averageValueLanguage()
  );

  const queryAmountSkillUser = useQuery(
    ["statistic", "amount-skill-user"],
    () => statisticApi.amountSkillUser()
  );

  const queryJobLevelCount = useQuery(["statistic", "job-level-count"], () =>
    statisticApi.jobLevelCount()
  );

  return (
    <>
      {queryMostUsedLanguage.data && (
        <Bar
          options={{
            indexAxis: "x",
            responsive: true,
            plugins: {
              title: {
                display: true,
                text: "Habilidades mais usadas",
              },
            },
            scales: {
              y: {
                ticks: {
                  precision: 0,
                },
              },
            },
            aspectRatio: 4,
          }}
          data={{
            labels: ["Quantidade"],
            datasets: queryMostUsedLanguage.data.map((x) => ({
              label: x.skill,
              data: [x.count],
              backgroundColor: getRandomRgb(),
              borderColor: "#000",
              maxBarThickness: 50,
            })),
          }}
        />
      )}
      {queryLessUsedLanguage.data && (
        <Bar
          options={{
            indexAxis: "x",
            responsive: true,
            plugins: {
              title: {
                display: true,
                text: "Habilidades menos usadas",
              },
            },
            scales: {
              y: {
                ticks: {
                  precision: 0,
                },
              },
            },
            aspectRatio: 4,
          }}
          data={{
            labels: ["Quantidade"],
            datasets: queryLessUsedLanguage.data.map((x) => ({
              label: x.skill,
              data: [x.count],
              backgroundColor: getRandomRgb(),
              borderColor: "#000",
              maxBarThickness: 50,
            })),
          }}
        />
      )}
      {queryAverageValueLanguage.data && (
        <Bar
          options={{
            indexAxis: "x",
            responsive: true,
            plugins: {
              title: {
                display: true,
                text: "Valor médio esperado por linguagem",
              },
            },
            scales: {
              y: {
                ticks: {
                  precision: 0,
                },
              },
            },
            aspectRatio: 4,
          }}
          data={{
            labels: ["Valor"],
            datasets: queryAverageValueLanguage.data.map((x) => ({
              label: x.skill,
              data: [x.value],
              backgroundColor: getRandomRgb(),
              borderColor: "#000",
              maxBarThickness: 50,
            })),
          }}
        />
      )}
      {queryAmountSkillUser.data && (
        <Bar
          options={{
            indexAxis: "x",
            responsive: true,
            plugins: {
              title: {
                display: true,
                text: "Quantidade de habilidades por usuário",
              },
            },
            scales: {
              y: {
                ticks: {
                  precision: 0,
                },
              },
            },
            aspectRatio: 4,
          }}
          data={{
            labels: ["Quantidade"],
            datasets: queryAmountSkillUser.data.map((x) => ({
              label: x.skill,
              data: [x.count],
              backgroundColor: getRandomRgb(),
              borderColor: "#000",
              maxBarThickness: 50,
            })),
          }}
        />
      )}
      {queryJobLevelCount.data && (
        <Bar
          options={{
            indexAxis: "x",
            responsive: true,
            plugins: {
              title: {
                display: true,
                text: "Quantidade de vagas por nível",
              },
            },
            scales: {
              y: {
                ticks: {
                  precision: 0,
                },
              },
            },
            aspectRatio: 4,
          }}
          data={{
            labels: ["Quantidade"],
            datasets: queryJobLevelCount.data.map((x) => ({
              label: skillLevelText[x.level],
              data: [x.count],
              backgroundColor: getRandomRgb(),
              borderColor: "#000",
              maxBarThickness: 50,
            })),
          }}
        />
      )}
      {queryJobLevelCount.data && (
        <Bar
          options={{
            indexAxis: "x",
            responsive: true,
            plugins: {
              title: {
                display: true,
                text: "Valor médio de vagas por nível",
              },
            },
            scales: {
              y: {
                ticks: {
                  precision: 0,
                },
              },
            },
            aspectRatio: 4,
          }}
          data={{
            labels: ["Valor"],
            datasets: queryJobLevelCount.data.map((x) => ({
              label: skillLevelText[x.level],
              data: [x.value],
              backgroundColor: getRandomRgb(),
              borderColor: "#000",
              maxBarThickness: 50,
            })),
          }}
        />
      )}
    </>
  );
};

export default StatisticsPage;
